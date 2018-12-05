#include <fstream>
#include <iostream>
#include <algorithm>
#include <vector>
#include <map>


using namespace std;

struct Node
{
	char c = NULL;
	int w = 0;
	Node *parent = NULL;
	Node *leftChild = NULL;
	Node *rightChild = NULL;
};

void incWeight(Node* root)
{
	root->w++;
	if (root->parent != NULL)
		incWeight(root->parent);
}

void checkChilds(Node* parent)
{
	if (parent->leftChild->w > parent->rightChild->w)
	{
		swap(parent->leftChild, parent->rightChild);
	}
}

void switchNodes(Node* n1, Node* n2)
{
	if (n1->parent->leftChild == n1) {
		n1->parent->leftChild = n2;
	}
	else {
		n1->parent->rightChild = n2;
	}

	if (n2->parent->leftChild == n2) {
		n2->parent->leftChild = n1;
	}
	else {
		n2->parent->rightChild = n1;
	}

	swap(n1->parent, n2->parent);
	checkChilds(n1->parent);
}

void recount(Node* escp)
{
	if (escp != NULL) {
		escp->w = escp->leftChild->w + escp->rightChild->w;
		recount(escp->parent);
	}
}


vector<Node*> sortTree(vector<Node*> tree)
{
	bool n = false;
	int ind;
	for (int i = tree.size() - 1; i > 0; i--) {
		for (int j = i - 1; j > -1; j--) {
			if (tree[i]->w > tree[j]->w) {
				n = true;
				ind = j;
			}
		}
		if (n) {
			switchNodes(tree[i], tree[ind]);
			swap(tree[i], tree[ind]);
			break;
		}
	}
	return tree;
}

vector<bool> getCode(Node* n, vector<bool> code) {
	if (n->parent == NULL)
		return code;
	if (n->parent->leftChild == n) {
		code.push_back(0);
	}
	else {
		code.push_back(1);
	}
	if (n->parent->parent != NULL)
		code = getCode(n->parent, code);
	return code;
}

void outCode(vector<bool> code)
{
	for (int i = code.size() - 1; i > -1; i--)
	{
		cout << code[i];
	}
}

int main()
{
	char buf;
	ifstream file("test.txt");
	vector<Node*> tree;
	map<char, Node*> leaves;
	Node *root = new Node;
	Node *esc = root;
	tree.push_back(root);
	file >> buf;
	while (file >> buf) {
		if (leaves[buf] != NULL) {
			incWeight(leaves[buf]);

			outCode(getCode(leaves[buf], vector<bool>()));
		}
		else {
			leaves[buf] = new Node;
			leaves[buf]->c = buf;
			leaves[buf]->parent = esc;

			Node *tmp = new Node;
			tmp->parent = esc;

			esc->leftChild = tmp;
			esc->rightChild = leaves[buf];
			tree.push_back(leaves[buf]);
			tree.push_back(tmp);

			outCode(getCode(esc, vector<bool>()));
			cout << buf;

			esc = tmp;
			incWeight(leaves[buf]);
		}
		tree = sortTree(tree);
		recount(esc->parent);
	}
	file.close();
	system("pause");
	return 0;
}