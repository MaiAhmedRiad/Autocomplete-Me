using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutocompleteMe
{
    public partial class search_form : Form
    {
        public string[] dictionary;
        public int stringIndex, wordIndex;
        public int C_index, U_index;
        public KeyValuePair<string, int>[] dataset;
        public List<KeyValuePair<string, int>> suggestions, tempList;
        public Result[] corrections;
        public List<string> UserInput;
        public List<string> CorrectedInput;
        public char nextChar;
        public string UpdatedString;
        public bool FirstSpace;
        public int counter;
        public string temp;
        public string WithoutSpecialC;
        public string SpecialChars;
        public bool subStringSearch;

        public search_form()
        {
            InitializeComponent();

            WordsDictionary.readDictionary("Dictionary (Large).txt");
            dictionary = WordsDictionary.wordsDictionary;
            WordsDictionary.dictionaryMergeSort(dictionary, 0, dictionary.Length - 1);

            Dataset.readDataset("Search Links (Large).txt");
            dataset = Dataset.dataset;
            Dataset.datasetMergeSort(dataset, 0, dataset.Length - 1);

            UserInput = new List<string>();
            CorrectedInput = new List<string>();
            subStringSearch = false;
            FirstSpace = false;
            C_index = 0;
            U_index = 0;
            counter = 0;
        }

        private void search_textBox_TextChanged(object sender, EventArgs e)
        {
            if (nextChar == (char)Keys.Back) //if the user presses backspace
            {
                if (UserInput.Count == 1)
                {
                    UserInput[U_index] = UserInput[U_index].Substring(0, UserInput[U_index].Length - 1);
                    CorrectedInput[C_index] = UserInput[U_index];
                    if (UserInput[U_index].Length == 0)
                    {
                        UserInput.Remove(UserInput[U_index]);
                        CorrectedInput.Remove(CorrectedInput[C_index]);
                    }
                }//end if

                else if (UserInput.Count > 1)
                {
                    UserInput[U_index] = UserInput[U_index].Substring(0, UserInput[U_index].Length - 1);
                    CorrectedInput[C_index] = UserInput[U_index];
                    if (UserInput[U_index].Length == 0)
                    {
                        UserInput.Remove(UserInput[U_index]);
                        U_index--;
                        CorrectedInput.Remove(CorrectedInput[C_index]);
                        C_index--;
                    }

                }//end else if
            }//end if
            else if (nextChar == (char)Keys.Space)
            {
                UserInput.Add(" ");
                U_index += 1;
                CorrectedInput.Add(" ");
                C_index += 1;
            }//end else if
            else
            {
                if (UserInput.Count == 0)
                {
                    UserInput.Add(nextChar.ToString().ToLower());
                    CorrectedInput.Add(nextChar.ToString().ToLower());
                }
                else if (UserInput[U_index] == " ")
                {
                    U_index++;
                    C_index++;
                    UserInput.Add(nextChar.ToString().ToLower());
                    CorrectedInput.Add(nextChar.ToString().ToLower());
                }
                else
                {
                    UserInput[U_index] += nextChar.ToString().ToLower();
                    CorrectedInput[C_index] = UserInput[U_index];
                }
            }//end else
            //----------------------------------------------------------------------------------------------------------------------------
            if (search_textBox.Text == "")
            {
                bubbleSort_button.Hide();
                mergeSort_button.Hide();
                suggestions_listBox.Items.Clear();
                suggestions_listBox.Visible = false;
                subStringSearch = false;
            }//end if
            else
            {
                bubbleSort_button.Show();
                mergeSort_button.Show();

                UpdatedString = "";
                foreach (string s in CorrectedInput)
                {
                    if (s == " ")
                    {
                        if (FirstSpace == false)
                        {
                            UpdatedString += s;
                            FirstSpace = true;
                        }
                    }
                    else
                    {
                        FirstSpace = false;
                        UpdatedString += s;
                    }

                }

                if (subStringSearch == false)
                {
                    stringIndex = Dataset.prefixBinarySearch(dataset, dataset.Length, UpdatedString);
                    if (stringIndex != -1)
                    {
                        suggestions_listBox.Items.Clear();
                        suggestions = Dataset.fetchSuggestions(dataset, UpdatedString);
                        int c = 0;
                            foreach (KeyValuePair<string, int> pair in suggestions)
                            {
                                suggestions_listBox.Items.Add(pair.Key);
                                c++;
                                if(c==10)
                                {
                                    break;
                                }
                            }

                        suggestions_listBox.Visible = true;
                    }//end if
                    //----------------------------------------------------------------------------------------------------------------------------
                    else //didn't find a match for prefix search in the dataset
                    {
                        suggestions_listBox.Items.Clear();
                        suggestions_listBox.Visible = false;
                        counter = 0;
                        for (int i = C_index; i >= 0; i--)
                        {
                            if (CorrectedInput[i] == " ")
                                counter++;
                            else
                                break;
                        }
                        ///////////////////////////////////////////
                        bool end = false;
                        int temp = -1;
                        SpecialChars = "";
                        WithoutSpecialC = "";

                        for (int i = 0; i < CorrectedInput[C_index - counter].Length; i++)
                        {
                            if ((CorrectedInput[C_index - counter][i] >= 97 && CorrectedInput[C_index - counter][i] <= 122))
                            {
                                if (end == false)
                                {

                                    SpecialChars += 'b';
                                    temp = SpecialChars.Length;
                                    end = true;
                                }
                                WithoutSpecialC += CorrectedInput[C_index - counter][i];


                            }
                            else
                            {

                                SpecialChars += CorrectedInput[C_index - counter][i];
                            }

                        }
                        if (end == false)
                        {
                            temp = SpecialChars.Length;
                        }
                        /////////////////////////////////////////////////////
                        if (WithoutSpecialC.Length != 0)
                        {


                            wordIndex = WordsDictionary.binarySearch(dictionary.Length, WithoutSpecialC);

                            if (wordIndex == -1) //the user enters a wrong word
                            {
                                string SpecialTemp;
                                corrections = WordsDictionary.closestWords(WithoutSpecialC);

                                for (int j = corrections.Length - 1; j >= 0; j--)
                                {
                                    SpecialTemp = SpecialChars;
                                    CorrectedInput[C_index - counter] = corrections[j].sentence;

                                    int x = temp;

                                    for (; x < SpecialChars.Length; x++)
                                    {
                                        CorrectedInput[C_index - counter] += SpecialChars[x];
                                    }
                                    SpecialTemp = SpecialTemp.Substring(0, temp - 1);
                                    SpecialTemp += CorrectedInput[C_index - counter];
                                    CorrectedInput[C_index - counter] = SpecialTemp;

                                    UpdatedString = "";
                                    foreach (string s in CorrectedInput)
                                    {
                                        if (s == " ")
                                        {
                                            if (FirstSpace == false)
                                            {
                                                UpdatedString += s;
                                                FirstSpace = true;
                                            }
                                        }
                                        else
                                        {
                                            FirstSpace = false;
                                            UpdatedString += s;
                                        }
                                    }
                                    stringIndex = Dataset.prefixBinarySearch(dataset, dataset.Length, UpdatedString);
                                    if (stringIndex != -1)
                                        break;
                                }


                                if (stringIndex != -1)
                                {
                                    suggestions_listBox.Items.Clear();
                                    suggestions = Dataset.fetchSuggestions(dataset, UpdatedString);
                                    int c = 0;
                                    foreach (KeyValuePair<string, int> pair in suggestions)
                                    {
                                        suggestions_listBox.Items.Add(pair.Key);
                                        c++;
                                        if (c == 10)
                                        {
                                            break;
                                        }
                                    }

                                }//end if
                                else
                                {
                                    suggestions_listBox.Items.Add("No results found");
                                    bubbleSort_button.Hide();
                                    mergeSort_button.Hide();
                                }

                                suggestions_listBox.Visible = true;

                            }//end if (wordIndex == -1)


                            else //user types correct words, but no suggestions are found, so do substring search
                            {
                                suggestions_listBox.Items.Clear();

                                // mile stone 2  
                                suggestions = Dataset.substring(UpdatedString);
                                subStringSearch = true;
                                // if no result found
                                // bonus 
                                string[] st = UpdatedString.Split();
                                List<KeyValuePair<string, int>> results = Dataset.substring(st[0]);

                                suggestions = Dataset.subsequnce(UpdatedString, results);

                                if (suggestions.Count != 0)
                                {
                                    suggestions_listBox.Items.Clear();
                                    int c = 0;
                                    foreach (KeyValuePair<string, int> pair in suggestions)
                                    {
                                        suggestions_listBox.Items.Add(pair.Key);
                                        c++;
                                        if (c == 10)
                                        {
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    suggestions_listBox.Items.Add("No results found");
                                    bubbleSort_button.Hide();
                                    mergeSort_button.Hide();    
                                }

                                suggestions_listBox.Visible = true;
                            }//end else
                        }//end else
                    }//end if(subStringSearch==false)
                }
                else //if substring search succeeded at a letter, continue with it till the end, we are sure prefix won't succeed now
                {
                    suggestions_listBox.Items.Clear();
                    UpdatedString = "";
                    foreach (string s in UserInput)
                    {
                        if (s == " ")
                        {
                            if (FirstSpace == false)
                            {
                                UpdatedString += s;
                                FirstSpace = true;
                            }
                        }
                        else
                        {
                            FirstSpace = false;
                            UpdatedString += s;
                        }
                    }
                    // mile stone 2  
                    suggestions = Dataset.substring(UpdatedString);

                    // if no result found
                    // bonus 
                    string[] st = UpdatedString.Split();
                    List<KeyValuePair<string, int>> results = Dataset.substring(st[0]);

                    suggestions = Dataset.subsequnce(UpdatedString, results);

                    if (suggestions.Count != 0)
                    {
                        suggestions_listBox.Items.Clear();
                        int c = 0;
                        foreach (KeyValuePair<string, int> pair in suggestions)
                        {
                            suggestions_listBox.Items.Add(pair.Key);
                            c++;
                            if (c == 10)
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        suggestions_listBox.Items.Add("No results found");
                        bubbleSort_button.Hide();
                        mergeSort_button.Hide();
                    }

                    suggestions_listBox.Visible = true;
                }
            }
        }

        private void bubbleSort_button_Click(object sender, EventArgs e)
        {
            suggestions_listBox.Items.Clear();

            //if (suggestions.Count != 0)
            //{
            Result[] results = new Result[suggestions.Count];

            for (int i = 0; i < suggestions.Count; i++)
            {
                results[i] = new Result();
                results[i].sentence = suggestions[i].Key;
                results[i].weight = suggestions[i].Value;
            }//end for

            Sort.bubbleSort(ref results);

            suggestions_listBox.Visible = true;

            foreach (Result r in results)
                suggestions_listBox.Items.Add(r.sentence);
            //}//end if
        }

        private void mergeSort_button_Click(object sender, EventArgs e)
        {
            suggestions_listBox.Items.Clear();

            Result[] results = new Result[suggestions.Count];

            for (int i = 0; i < suggestions.Count; i++)
            {
                results[i] = new Result();
                results[i].sentence = suggestions[i].Key;
                results[i].weight = suggestions[i].Value;
            }//end for

            Sort.mergeSort(ref results, 0, results.Length - 1);

            suggestions_listBox.Visible = true;

            foreach (Result r in results)
                suggestions_listBox.Items.Add(r.sentence);
        }

        private void search_textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            nextChar = (char)e.KeyChar;
        }

        private void search_form_Load(object sender, EventArgs e)
        {
            bubbleSort_button.Hide();
            mergeSort_button.Hide();
        }

    }
}