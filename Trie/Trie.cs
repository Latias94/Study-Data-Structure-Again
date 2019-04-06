using System.Collections.Generic;

namespace Trie
{
    /// <summary>
    /// Trie 又叫字典树、前缀树
    /// Trie 的基本实现，只针对英文单词
    /// </summary>
    public class Trie
    {
        private class Node {
            /// <summary>
            /// 第二层节点到当前节点是不是一个单词，比如 cat 和 category 
            /// </summary>
            public bool isWord;

            /// <summary>
            /// 指向下一个节点的 map 指针，因为一个节点可能有多个子节点
            /// 这里只针对英文的 26 个字母，也可以用数组来存
            /// 其他结构根据需求来修改存储结构
            /// </summary>
            public SortedDictionary<char, Node> next;

            public Node(bool isWord) {
                this.isWord = isWord;
                next = new SortedDictionary<char, Node>();
            }

            public Node():this(false) {
            }
        }
        
        private Node root;

        private int size;

        public Trie() {
            root = new Node();
            size = 0;
        }

        /// <summary>
        /// Trie 树中有多少个单词
        /// </summary>
        public int GetSize() {
            return size;
        }
        
        /// <summary>
        /// 向 Trie 中添加一个新的单词
        /// </summary>
        /// <param name="word">要添加的单词</param>
        public void Insert(string word) {
            Node cur = root;
            for (int i = 0; i < word.Length; i++) {
                char c = word[i];
                // 当在当前节点指向的孩子节点中不存在要插入的字符 c 的时候，才执行插入动作
                if (!cur.next.ContainsKey(c)) {
                    cur.next.Add(c, new Node());
                }
                // 当在当前节点指向的孩子节点中存在要插入的字符 c 的时候，继续处理下一个字母
                cur = cur.next[c];
            }
            // 先判断这个单词是不是以前就存在
            if (!cur.isWord) {
                // 插入单词后，把这个单词插入后的末尾节点标记为是单词
                cur.isWord = true;
                size++;
            }
        }
        
        /// <summary>
        /// 查询单词是否在 Trie 树中
        /// </summary>
        /// <param name="word">要查询的单词</param>
        /// <returns>是否包含指定单词</returns>
        public bool Contain(string word) {
            Node cur = root;
            for (int i = 0; i < word.Length; i++) {
                char c = word[i];
                // 只要在第二层找不到 word 的第一个字符(第二层是所有单词的起点)
                if (!cur.next.ContainsKey(c)) {
                    return false;
                }
                cur = cur.next[c];
            }
            // 到达字符串的最后一个字符，也只说明包含了要查询单词的所有字母
            // 还要根据 isWord 来判断是否是一个单词
            return cur.isWord;
        }
        
        /// <summary>
        /// 判断某个字符串是否是单词的前缀(即某个字符串以这个字符串开始)
        /// </summary>
        /// <param name="prefix">前缀</param>
        /// <returns>是否包含这个前缀</returns>
        public bool StartsWith(string prefix) {
            Node cur = root;
            for (int i = 0; i < prefix.Length; i++) {
                char c = prefix[i];
                if (!cur.next.ContainsKey(c)) {
                    return false;
                }
                cur = cur.next[c];
            }
            // 不需要判断是否是单词
            return true;
        }
    }
}