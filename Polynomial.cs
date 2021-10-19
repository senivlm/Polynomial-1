using System;
using System.Collections.Generic;
using System.Text;

namespace Polynomial
{ треба розмовляти зі мною
    class Polynomial
    {
        Pol[] polynomial;
        int elemCount;
        

        public Polynomial(int degree)
        {
            elemCount = degree;
            polynomial = new Pol[elemCount];
            for(int i=0; i < elemCount; ++i)
            {
                polynomial[i] = new Pol(1, 5-i);
            }
        }

        public void Multiply(double mult)
        {
            for(int i=0; i<elemCount; ++i)
            {
                polynomial[i].Number *= mult;
            }
        }

        public void Parse(string s)
        {

        }

        public int ElemCount
        {
            get
            {
                return elemCount;
            }
            set
            {
                elemCount = value;
            }
        }
        public Pol this[int i]
        {
            get
            {
                return polynomial[i];
            }
            set
            {
                polynomial[i] = value;
            }
        }
        public Pol this [int index, double val]
        {
            get
            {
                return polynomial[index];
            }
            set
            {
                if (val != 0 && elemCount > index)
                {

                    polynomial[index].Number = val;
                }
                else if (val != 0 && elemCount <= index)
                {
                    Pol[] arr = new Pol[elemCount+1];
                    for (int i = 0; i < elemCount; ++i)
                    {
                        arr[i] = polynomial[i];
                    }
                    arr[elemCount].Number = val;
                    arr[elemCount].Degree = index;

                    elemCount++;
                    polynomial = new Pol[index];
                    for (int i = 0; i < elemCount; ++i)
                    {
                        polynomial[i] = new Pol(arr[i]);
                    }
                }
                else if (val == 0 && elemCount > index)
                {
                    Pol[] arr = new Pol[elemCount -1];
                    for (int i = 0; i < elemCount; ++i)
                    {
                        if (index != polynomial[i].Degree)
                            arr[i] = polynomial[i];
                        else
                            i--;
                    }
                    elemCount--;
                    for (int i = 0; i < elemCount; ++i)
                    {
                        polynomial[i] = arr[i];
                    }
                }
                else if(val == 0 && elemCount <= index)
                {
                    return;
                }
            }
        }

        public override string ToString()
        {
            string rez = "";
            for(int i=0; i<elemCount; ++i)
            {
                if (polynomial[i].Number >= 0)
                {
                    if(i==0)
                        rez = rez  + polynomial[i].ToString();
                    else
                        rez = rez + "+" + polynomial[i].ToString();
                }
                else
                {
                    rez = rez + polynomial[i].ToString();
                }
            }
            return rez;
        }

        public Polynomial Add(Polynomial other)
        {
            int rezDegree = 0;
            if (elemCount > other.elemCount)
                rezDegree = elemCount;
            else
                rezDegree = other.elemCount;

            Polynomial rezult = new Polynomial(rezDegree);
            int i = 0, f=0, s=0;
            while (i < rezDegree && f<elemCount && s<other.elemCount)
            {
                if (polynomial[f].Degree == other[s].Degree)
                {
                    rezult[i] = new Pol(polynomial[f] + other[s]);
                    ++i;
                    ++f;
                    ++s;
                }
                else if(polynomial[f].Degree > other[s].Degree)
                {
                    rezult[i] = new Pol(other[s]);
                    i++;
                    s++;
                }
                else if(polynomial[f].Degree < other[s].Degree)
                {
                    rezult[i] = new Pol(polynomial[s]);
                    i++;
                    f++;
                }

            }

            if (f < elemCount || s < other.elemCount)
            {
                if (f < elemCount)
                {
                    while (f < elemCount)
                    {
                        rezult[i] = new Pol(polynomial[f]);
                        i++;
                        f++;
                    }
                }
                else
                {
                    while (s < other.elemCount)
                    {
                        rezult[i] = new Pol(other[s]);
                        i++;
                        s++;
                    }
                }
            }

            return rezult;
        }


        public static Polynomial operator +(Polynomial first, Polynomial second)
        {
            return first.Add(second);
        }

        public void Sort()
        {
            int min, mIndex = 0;
            Pol temp = new Pol(0,0);
            for (int i = 0; i < elemCount - 1; ++i)
            {
                temp = polynomial[i];
                min = polynomial[i + 1].Degree;
                mIndex = i + 1;
                for (int k = i + 1; k < polynomial.Length; k++)
                {
                    if (min > polynomial[k].Degree)
                    {
                        min = polynomial[k].Degree;
                        mIndex = k;
                    }
                }
                if (temp.Degree > polynomial[mIndex].Degree)
                {
                    polynomial[i] = polynomial[mIndex];
                    polynomial[mIndex] = temp;
                }
            }

        }

        public void AddSummands()
        {
            Polynomial temp;
            int fl = 1;
            while (fl != 0)
            {
                fl = 0;
                for (int i = 1; i < elemCount; ++i)
                {
                    if (polynomial[i].Degree == polynomial[i - 1].Degree)
                    {
                        fl = 1;
                        temp = new Polynomial(elemCount - 1);
                        for (int j = 0; j < elemCount - 1; ++j)
                        {
                            if (j == i - 1)
                                temp[j] = polynomial[j] + polynomial[j + 1];
                            else if (j < i - 1)
                                temp[j] = polynomial[j];
                            else
                                temp[j] = polynomial[j + 1];

                        }

                        elemCount--;
                        polynomial = new Pol[elemCount];
                        polynomial = temp.polynomial;
                    }
                }
            }
        }

        public Polynomial Mult(Polynomial other)
        {
            int count = elemCount * other.elemCount;
            Polynomial rezult = new Polynomial(count);

            
            for(int i=0, r = 0; i <elemCount; ++i)
            {
                for(int j=0; j<other.elemCount; ++j)
                {
                    rezult[r] = polynomial[i] * other.polynomial[j];
                    ++r;
                }
            }

            rezult.Sort();
            rezult.AddSummands();

            return rezult;
        }

        public static Polynomial operator *(Polynomial first, Polynomial second)
        {
            return first.Mult(second);
        }

        public void DeleteZero()
        {
            Polynomial temp;
            int fl = 1;
            while (fl != 0)
            {
                fl = 0;
                for (int i = 0; i < elemCount; ++i)
                {
                    if (polynomial[i].Number == 0)
                    {
                        fl = 1;
                        temp = new Polynomial(elemCount - 1);
                        for (int j = 0; j < elemCount - 1; ++j)
                        {
                            if (j >= i)
                                temp[j] = polynomial[j + 1];
                            else if (j < i)
                                temp[j] = polynomial[j];
                            //else
                            //    temp[j] = polynomial[j + 1];
                        }

                        elemCount--;
                        polynomial = new Pol[elemCount];
                        polynomial = temp.polynomial;
                    }
                }
            }
        }

        public Polynomial Subtract(Polynomial other)
        {
            int rezDegree = 0;
            if (elemCount > other.elemCount)
                rezDegree = elemCount;
            else
                rezDegree = other.elemCount;

            Polynomial rezult = new Polynomial(rezDegree);
            int i = 0, f = 0, s = 0;
            while (i < rezDegree && f < elemCount && s < other.elemCount)
            {
                if (polynomial[f].Degree == other[s].Degree)
                {
                    rezult[i] = new Pol(polynomial[f] - other[s]);
                    ++i;
                    ++f;
                    ++s;
                }
                else if (polynomial[f].Degree > other[s].Degree)
                {
                    rezult[i] = new Pol(other[s]*(-1));
                    i++;
                    s++;
                }
                else if (polynomial[f].Degree < other[s].Degree)
                {
                    rezult[i] = new Pol(polynomial[s]);
                    i++;
                    f++;
                }

            }

            if (f < elemCount || s < other.elemCount)
            {
                if (f < elemCount)
                {
                    while (f < elemCount)
                    {
                        rezult[i] = new Pol(polynomial[f]);
                        i++;
                        f++;
                    }
                }
                else
                {
                    while (s < other.elemCount)
                    {
                        rezult[i] = new Pol(other[s]*(-1));
                        i++;
                        s++;
                    }
                }
            }
            rezult.DeleteZero();
            return rezult;
        }
    }
}
