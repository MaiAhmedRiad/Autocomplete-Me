using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO; //for reading files

namespace AutocompleteMe
{
    class Dataset
    {
        public static KeyValuePair<string, int>[] dataset;

        public static void readDataset(string fileName)
        {
            try
            {
                FileStream fr = new FileStream(fileName, FileMode.Open);
                StreamReader sr = new StreamReader(fr);
                int counter, index = 0, weight;
                int.TryParse(sr.ReadLine(), out counter);
                dataset = new KeyValuePair<string, int>[counter];
                char[] delimeter = new char[1] { ',' };
                string[] weightAndWord = new string[2];

                while (sr.Peek() != -1)
                {
                    weightAndWord = sr.ReadLine().Split(delimeter, 2); //split to two substrings only
                    int.TryParse(weightAndWord[0], out weight);
                    dataset[index] = new KeyValuePair<string, int>(weightAndWord[1].ToLower(), weight);
                    index++;
                }//end while

                sr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }//end readDataset

        public static void datasetMergeSort(KeyValuePair<string, int>[] data, int left, int right)
        {
            if (left < right)
            {
                int m = left + (right - left) / 2;

                datasetMergeSort(data, left, m);
                datasetMergeSort(data, m + 1, right);
                datasetMerge(data, left, m, right);
            }//end if
        }//end mergesort

        private static void datasetMerge(KeyValuePair<string, int>[] data, int left, int mid, int right)
        {
            int i, j, k;
            int nLeft = mid - left + 1;
            int nright = right - mid;
            KeyValuePair<string, int>[] arrLeft = new KeyValuePair<string, int>[nLeft];
            KeyValuePair<string, int>[] arrRight = new KeyValuePair<string, int>[nright];

            for (i = 0; i < nLeft; i++)
                arrLeft[i] = data[left + i];

            for (j = 0; j < nright; j++)
                arrRight[j] = data[mid + 1 + j];

            i = 0;
            j = 0;
            k = left;

            while (i < nLeft && j < nright)
            {
                bool l = false, r = false;
                for (int x = 0; x < Math.Min(arrLeft[i].Key.Length, arrRight[j].Key.Length); x++)
                {
                    if (arrLeft[i].Key[x] < arrRight[j].Key[x])
                    {
                        l = true;
                        break;
                    }
                    else if (arrLeft[i].Key[x] > arrRight[j].Key[x])
                    {
                        r = true;
                        break;
                    }
                }
                if (l == true)
                {
                    data[k] = arrLeft[i];
                    i++;
                }
                else if (r == true)
                {
                    data[k] = arrRight[j];
                    j++;
                }
                else if (l == false && r == false)
                {
                    if (arrLeft[i].Key.Length < arrRight[j].Key.Length)
                    {
                        data[k] = arrLeft[i];
                        i++;
                    }
                    else
                    {
                        data[k] = arrRight[j];
                        j++;
                    }
                }
                k++;
            }

            while (i < nLeft)
            {
                data[k] = arrLeft[i];
                i++;
                k++;
            }//end while

            while (j < nright)
            {
                data[k] = arrRight[j];
                j++;
                k++;
            }//end while
        }//end merge

        public static int prefixBinarySearch(KeyValuePair<string, int>[] sorteddataset, int size, string searchValue)
        {
            int first = 0,         //First array element
            last = size - 1,       //Last array element
            mid,                   //Mid point of search
            index = -1;            //Position of search value
            bool found = false;    //Flag

            while (!found && first <= last)
            {
                mid = (first + last) / 2;

                if (searchValue == sorteddataset[mid].Key)
                {
                    found = true;
                    index = mid;
                    return index;
                }//end if
                else
                {
                    bool match = true;
                    for (int i = 0; i < Math.Min(searchValue.Length, sorteddataset[mid].Key.Length); i++)
                    {

                        if (sorteddataset[mid].Key[i] != searchValue[i])
                        {
                            if (searchValue[i] < sorteddataset[mid].Key[i])
                            {
                                last = mid - 1;
                                match = false;
                                break;
                            }
                            else
                            {
                                first = mid + 1;
                                match = false;
                                break;
                            }
                        }//end if
                    }//end for
                    if (match == true)
                    {
                        if (searchValue.Length < sorteddataset[mid].Key.Length)
                            return mid;

                        else
                            first = mid + 1;
                    }//end if
                }//end else
            }//end while

            return index;
        }//end prefixbinarysearch

        private static bool prefixStringSearch(string dataString, string searchValue)
        {
            if (searchValue == dataString)
                return true;
            else
            {
                if (searchValue.Length < dataString.Length)
                {
                    int i;
                    for (i = 0; i < searchValue.Length; i++)
                    {
                        if (dataString[i] != searchValue[i])
                            return false;
                    }//end for
                    return true;
                }//end if
                return false;
            }//end else

        }//end prefixSearch

        public static List<KeyValuePair<string, int>> fetchSuggestions(KeyValuePair<string, int>[] sorteddataset, string searchValue)
        {
            List<KeyValuePair<string, int>> suggestions = new List<KeyValuePair<string, int>>();

            int index = prefixBinarySearch(sorteddataset, sorteddataset.Length, searchValue);

            if (index != -1)
            {

                int upperPartIndex = index - 1, lowerPartIndex = index + 1;

                while (upperPartIndex >= 0)
                {
                    if (prefixStringSearch(sorteddataset[upperPartIndex].Key, searchValue) == true)

                        suggestions.Add(sorteddataset[upperPartIndex]);

                    else
                        break;

                    upperPartIndex--;

                }//end while

                while (lowerPartIndex <= (sorteddataset.Length - 1))
                {
                    if (prefixStringSearch(sorteddataset[lowerPartIndex].Key, searchValue) == true)

                        suggestions.Add(sorteddataset[lowerPartIndex]);

                    else
                        break;

                    lowerPartIndex++;

                }//end while

                suggestions.Add(sorteddataset[index]);
            }//end if

            return suggestions;

        }//end fetchSuggestions

        public static bool ifsubstring(string s1, string s2)
        { 		// s1 is the the string , s2 is the substring 

            int i = 0, j = 0;

            for (int x = 0; x < s1.Length; x++)
            {

                if (s1[j] == s2[i])
                {
                    i++;
                    j++;
                }

                else
                {
                    i = 0;

                    if (s1[j] == s2[i])
                    {
                        i++;
                        j++;
                    }
                    else
                        j++;
                }


                if (i == s2.Length)
                    return true;
            }//endnestedfor

            return false;
        }//end ifsubstring

        public static List<KeyValuePair<string, int>> substring(string searchValue)
        {
            List<KeyValuePair<string, int>> suggestions = new List<KeyValuePair<string, int>>();
            foreach (var item in dataset)
            {
                if (ifsubstring(item.Key, searchValue))
                    suggestions.Add(item);
            }


            return suggestions;
        } // end substring 

        public static List<KeyValuePair<string, int>> sub_string(string searchValue, List<KeyValuePair<string, int>> results)
        {
            List<KeyValuePair<string, int>> suggestions = new List<KeyValuePair<string, int>>();

            foreach (var item in results)
            {
                if (ifsubstring(item.Key, searchValue))
                    suggestions.Add(item);
            }


            return suggestions;
        } //

        public static List<KeyValuePair<string, int>> subsequnce(string searchValue, List<KeyValuePair<string, int>> result)
        {
            string[] st = searchValue.Split();

            for (int i = 1; i < st.Length; i++)
            {
                if (st[i] != "")
                    result = sub_string(st[i], result).Distinct().ToList();
            }

            return result;
        }// end subsequnce

    }//end class
}