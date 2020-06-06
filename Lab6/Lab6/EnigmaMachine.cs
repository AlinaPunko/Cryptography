using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    public class EnigmaMachine
    {
        private Dictionary<char, char> plugBoard;

        private Rotor[] rotors;
        private Rotor reflector;

        private const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private const string rotorIconf = "EKMFLGDQVZNTOWYHXUSPAIBRCJ";
        private const string rotorIIconf = "AJDKSIRUXBLHWTMCQGZNPYFVOE";
        private const string rotorIIIconf = "BDFHJLCPRTXVZNYEIWGAKMUSQO";
        private const string rotorBetaconf = "LEYJVCNIXWPBQMDRTAKZFGUHOS";
        private const string rotorGammaconf = "FSOKANUERHMBTIYCWLQPZXVGJD";
        private const string reflectorAconf = "EJMZALYXVBWFCRQUONTSPIKHGD";
        private const string reflectorBconf = "YRUHQSLDPXNGOKMIEBFZCWVJAT";
        private const string reflectorCconf = "FVPJIAOYEDRZXWGCTKUQSBNMHL";
        private class Rotor
        {
            private int outerPosition;
            public char outerChar { get; set; }
            private string wiring;
            private char turnOver;

            public string name { get; }

            public char ring { get; set; }

            public int[] map { get; }
            public int[] revMap { get; }

            public Rotor(string w, char to, string n)
            {
                turnOver = to;
                outerPosition = 0;

                ring = 'A';
                name = n;

                map = new int[26];
                revMap = new int[26];

                setWiring(w);
            }

            public void setWiring(string newW)
            {
                wiring = newW;
                outerChar = wiring.ToCharArray()[outerPosition];
                for (int i = 0; i < 26; i++)
                {
                    int match = ((int)wiring.ToCharArray()[i]) - 65;
                    map[i] = (26 + match - i) % 26;
                    revMap[match] = (26 + i - match) % 26;
                }
            }

            public void setOuterPosition(int i)
            {
                outerPosition = i;
                outerChar = alphabet.ToCharArray()[outerPosition];
            }

            public int getOuterPosition()
            {
                return outerPosition;
            }

            public void setOuterChar(char c)
            {
                outerChar = c;
                outerPosition = alphabet.IndexOf(outerChar);
            }

            public void step()
            {
                outerPosition = (outerPosition + 3) % 26;
                outerChar = alphabet.ToCharArray()[outerPosition];
            }
            public void step2()
            {
                outerPosition = (outerPosition + 1) % 26;
                outerChar = alphabet.ToCharArray()[outerPosition];
            }
            public void step3()
            {
                outerPosition = (outerPosition + 3) % 26;
                outerChar = alphabet.ToCharArray()[outerPosition];
            }
            public bool IsInTurnOver()
            {
                return outerChar == turnOver;
            }
        }

        private void rotateRotors(Rotor[] r)
        {
            if (r.Length != 3)
                return;
            r[2].step();
            r[1].step2();
            r[0].step3();
        }

        private char rotorMap(char c, bool reverse)
        {
            int cPos = (int)c - 65;
            if (!reverse)
            {
                for (int i = rotors.Length - 1; i >= 0; i--)
                {
                    cPos = rotorValue(rotors[i], cPos, reverse);
                }
            }
            else
            {
                cPos = rotors.Aggregate(cPos, (current, t) => rotorValue(t, current, reverse));
            }

            return alphabet.ToCharArray()[cPos];
        }

        private int rotorValue(Rotor r, int cPos, bool reverse)
        {
            int rPos = (int)r.ring - 65;
            int d;
            d = !reverse ? r.map[(26 + cPos + r.getOuterPosition() - rPos) % 26] : r.revMap[(26 + cPos + r.getOuterPosition() - rPos) % 26];

            return (cPos + d) % 26;
        }
        private char ReflectorMap(char c)
        {
            int cPos = (int)c - 65;
            cPos = (cPos + reflector.map[cPos]) % 26;
            return alphabet.ToCharArray()[cPos];
        }

        // Constructor
        public EnigmaMachine()
        {
            plugBoard = new Dictionary<char, char>();

            Rotor rI = new Rotor(rotorIconf, 'Q', "I");
            Rotor rII = new Rotor(rotorIIconf, 'E', "II");
            Rotor rIII = new Rotor(rotorIIIconf, 'V', "III");
            rotors = new Rotor[] { rI, rII, rIII };
            reflector = new Rotor(reflectorAconf, ' ', "");
        }

        public void SetReflector(char conf)
        {
            if (conf != 'A' && conf != 'B' && conf != 'C')
            {
                throw new ArgumentException("Invalid argument");
            }

            string wiring = "";
            switch (conf)
            {
                case 'A':
                    wiring = reflectorAconf;
                    break;
                case 'B':
                    wiring = reflectorBconf;
                    break;
                case 'C':
                    wiring = reflectorCconf;
                    break;
            }
            reflector.setWiring(wiring);
        }

        public void SetSettings(char[] rings, char[] grund)
        {
            if (rings.Length != rotors.Length || grund.Length != rotors.Length)
            {
                throw new ArgumentException("Invalid argument lengths");
            }

            for (int i = 0; i < rotors.Length; i++)
            {
                rotors[i].ring = char.ToUpper(rings[i]);
                rotors[i].setOuterChar(char.ToUpper(grund[i]));
            }
        }

        public void SetSettings(char[] rings, char[] grund, string rotorOrder)
        {
            Rotor rI = null;
            Rotor rII = null;
            Rotor rIII = null;
            Rotor rBeta = new Rotor(rotorBetaconf, 'Z', "Beta");
            Rotor rGamma = new Rotor(rotorGammaconf, 'Z', "Gamma");
            // Get the current ordering
            foreach (var t in rotors)
            {
                switch (t.name)
                {
                    case "I":
                        rI = t;
                        break;
                    case "II":
                        rII = t;
                        break;
                    case "III":
                        rIII = t;
                        break;
                }
            }

            string[] order = rotorOrder.Split('-');

            // Set the new ordering
            for (int i = 0; i < order.Length; i++)
            {
                if (order[i] == "I")
                    rotors[i] = rI;
                if (order[i] == "II")
                    rotors[i] = rII;
                if (order[i] == "III")
                    rotors[i] = rIII;
                if (order[i] == "Gamma" || order[i] == "GAMMA")
                    rotors[i] = rGamma;
                if (order[i] == "Beta" || order[i] == "BETA")
                    rotors[i] = rBeta;
            }

            SetSettings(rings, grund);
        }

        public void SetSettings(char[] rings, char[] grund, string rotorOrder, char reflectorConf)
        {
            SetReflector(reflectorConf);
            SetSettings(rings, grund, rotorOrder);
        }

        public string RunEnigma(string msg)
        {
            StringBuilder encryptedMessage = new StringBuilder();

            msg = msg.ToUpper();

            foreach (char c in msg)
            {
                encryptedMessage.Append(EncryptChar(c));
            }

            return encryptedMessage.ToString();
        }

        private char EncryptChar(char c)
        {
            rotateRotors(rotors);

            if (plugBoard.ContainsKey(c))
            {
                c = plugBoard[c];
            }

            c = rotorMap(c, false);
            c = ReflectorMap(c);
            c = rotorMap(c, true);

            if (plugBoard.ContainsKey(c))
            {
                c = plugBoard[c];
            }
            return c;
        }

        public void AddPlug(char c, char cc)
        {
            if (char.IsLetter(c) && char.IsLetter(cc))
            {
                c = char.ToUpper(c);
                cc = char.ToUpper(cc);
                if (c == cc || plugBoard.ContainsKey(c))
                    return;
                plugBoard.Add(c, cc);
                plugBoard.Add(cc, c);
            }
            else
            {
                throw new ArgumentException("Invalid character");
            }
        }
    }
}
