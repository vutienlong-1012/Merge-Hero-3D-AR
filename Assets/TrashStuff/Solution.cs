public class Solution
{
    public int RomanToInt(string s)
    {
        int _value = 0;
        char[] _split = s.ToCharArray();


        for (int i = 0; i < _split.Length; i++)
        {
            switch (_split[i])
            {
                case 'I':
                    if (i < _split.Length-1)
                    {
                        if (_split[i + 1] == 'V' || _split[i + 1] == 'X')
                            _value -= 1;
                        else
                            _value += 1;
                    }
                    else
                        _value += 1;
                    break;
                case 'V':
                    _value += 5;
                    break;
                case 'X':
                    if (i < _split.Length-1)
                    {
                        if (_split[i + 1] == 'L' || _split[i + 1] == 'C')
                            _value -= 10;
                        else
                            _value += 10;
                    }
                    else
                        _value += 10;
                    break;
                case 'L':
                    _value += 50;
                    break;
                case 'C':
                    if (i < _split.Length - 1)
                    {
                        if (_split[i + 1] == 'D' || _split[i + 1] == 'M')
                            _value -= 100;
                        else
                            _value += 100;
                    }
                    else
                        _value += 100;
                    break;
                case 'D':
                    _value += 500;
                    break;
                case 'M':
                    _value += 1000;
                    break;
            }
        }
        return _value;
    }
}
