using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibGit2Sharp;

namespace GoStore_FrontEnd
{
    class CommitsMap_Manager
    {
        private string              _repoPath;
        private CommitsMap          _cmtMap;
        private List<CommitNode>    _commits;


        internal class CommitNode : IComparable
        {
            public List<CommitNode> upperCommits;
            public string           text;
            public string           sha;
            public DateTime         time;

            public int CompareTo(object obj)
            {
                CommitNode node = obj as CommitNode;

                return -time.CompareTo(node.time);
            }
        }


        public CommitsMap_Manager(CommitsMap commitsMap, string repoPath)
        {
            _repoPath = repoPath;
            _commits = new List<CommitNode>();
            _cmtMap = commitsMap;
        }

        public void Load()
        {
            CommitsMap.TimeNode timeNode;
            CommitsMap.TimeNode upperTimeNode;
            List<CommitsMap.TimeNode> upperTimeNodes;

            try
            {
                _InvertCommitsTree();
                foreach(CommitNode node in _commits)
                {
                    timeNode = new CommitsMap.TimeNode();
                    timeNode.text = node.text;
                    timeNode.sha = node.sha;
                    timeNode.time = node.time;
                    
                    if (node.upperCommits == null)
                    {
                        _cmtMap.AddNode(timeNode);
                    }
                    else
                    {
                        upperTimeNodes = new List<CommitsMap.TimeNode>();
                        foreach(CommitNode upperNode in node.upperCommits)
                        {
                            upperTimeNode = _cmtMap.IsTimeNodeExist(upperNode.sha);
                            if(upperTimeNode != null)
                            {
                                upperTimeNodes.Add(upperTimeNode);
                            }
                        }

                        if (node.upperCommits.Count() == 1)
                            _cmtMap.AddNode(timeNode, upperTimeNodes[0]);
                        else
                            _cmtMap.AddNode(timeNode, upperTimeNodes);
                    }                    
                }
            }
            catch (Exception)
            {

            }
        }

        private void _InvertCommitsTree()
        {
            CommitNode curNode;
            CommitNode upperNode;

            try
            {
                _commits.Clear();

                using (var repo = new Repository(_repoPath))
                {
                    foreach (Commit cmt in repo.Commits)
                    {
                        upperNode = _IsCommitExists(cmt.Sha);
                        if(upperNode == null)
                        {
                            upperNode = new CommitNode();
                            upperNode.sha = cmt.Sha;
                            upperNode.text = cmt.Message;
                            upperNode.time = cmt.Committer.When.DateTime;

                            _commits.Add(upperNode);
                        }

                        foreach(Commit parentCmt in cmt.Parents)
                        {
                            curNode = _IsCommitExists(parentCmt.Sha);
                            if (null == curNode)
                            {
                                curNode = new CommitNode();
                                curNode.sha = parentCmt.Sha;
                                curNode.text = parentCmt.Message;
                                curNode.time = parentCmt.Committer.When.DateTime;


                                if (upperNode != null)
                                {
                                    if (null == curNode.upperCommits)
                                        curNode.upperCommits = new List<CommitNode>();

                                    curNode.upperCommits.Add(upperNode);
                                }

                                _commits.Add(curNode);
                            }
                            else
                            {
                                if (upperNode != null)
                                {
                                    if (null == curNode.upperCommits)
                                        curNode.upperCommits = new List<CommitNode>();

                                    curNode.upperCommits.Add(upperNode);
                                }
                            }

                        }

                    }

                    _commits.Sort();
                }
            }
            catch(Exception)
            {

            }           
        }

        private CommitNode _IsCommitExists(string sha)
        {
            foreach(CommitNode target in _commits)
            {
                if (sha == target.sha)
                    return target;
            }
            return null;
        }

    }
}
