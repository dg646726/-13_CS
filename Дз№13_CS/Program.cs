using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Дз_13_CS
{
    enum Type
    {
        Six, Seven, Eight, Nine, Ten, Valet, Dame, King, Ace
    }
    enum Suit
    {
        Worms, Bubi, Spades, Cross
    }
    class Kart
    {
        public Type TypeKart { get; set; }
        public Suit SuitKart { get; set; }
        public Kart(string type, string suit)
        {
            TypeKart = (Type)Enum.Parse(typeof(Type), type);
            SuitKart = (Suit)Enum.Parse(typeof(Suit), suit);
        }
    }
    class Player
    {
        public Queue<Kart> Karts { set; get; }

        public void ShowKarts()
        {
            foreach (Kart k in Karts)
            {
                Console.WriteLine($"Suit kart: {k.SuitKart}  Type kart: {k.TypeKart}");
            }
        }
    }
    class Game
    {
        private List<Player> players = new List<Player>();
        private Queue<Kart> cardsDeck = new Queue<Kart>();
        private int countPlayer;
        private int cartsInPlayerOnStart;
        public Game(int countPlayer)
        {
            this.countPlayer = countPlayer;
            for (int i = 0; i < countPlayer; i++)
            {
                players.Add(new Player());
            }
            MixingTheDeck();
            cartsInPlayerOnStart = cardsDeck.Count / players.Count;
            for (int i = 0; i < players.Count; i++)
            {
                players[i].Karts = GiveCardsForPlayer();
            }
        }
        public void MixingTheDeck()
        {
            for (int i = 0; i < cardsDeck.Count; i++)
            {
                cardsDeck.Dequeue();
            }
            int k1 = 0;
            int k2 = 0;
            int k3 = 0;
            int k4 = 0;
            int randomCard;
            Random rnd = new Random();
            string[] types = Enum.GetNames(typeof(Type));
            string[] suits = Enum.GetNames(typeof(Suit));

            for (; ; )
            {
                randomCard = rnd.Next(1000);
                if (randomCard < 250 && k3 < 9)
                {
                    cardsDeck.Enqueue(new Kart(types[k3],suits[2]));
                    k3++;
                }
                else if (randomCard > 750 && k1 < 9)
                {
                    cardsDeck.Enqueue(new Kart(types[k1], suits[0]));
                    k1++;
                }
                else if (randomCard <= 500 && randomCard >= 250 && k4 < 9)
                {
                    cardsDeck.Enqueue(new Kart(types[k4], suits[3]));
                    k4++;
                }
                else if (randomCard <= 750 && randomCard >= 500 && k2 < 9)
                {
                    cardsDeck.Enqueue(new Kart(types[k2], suits[1]));
                    k2++;
                }
                if (cardsDeck.Count == 36)
                    break;
            }
        }
        public Queue<Kart> GiveCardsForPlayer()
        {
            Queue<Kart> cards = new Queue<Kart>();
            for (int i = 0; i < cartsInPlayerOnStart; i++)
            {
                cards.Enqueue(cardsDeck.Dequeue());
            }
            return cards;
        }

        public void GamePlay()
        {
            int k = 0;
            Kart first;
            Kart second;
            for (;;)
            {
                first = players[k].Karts.Dequeue();
                Console.WriteLine($"#{k+1} player(kart: {first.TypeKart})");
                if (k == players.Count - 1)
                {
                    second = players[0].Karts.Dequeue();
                    Console.WriteLine($"#{1} player(kart: {second.TypeKart})");
                }
                else
                {
                    second = players[k + 1].Karts.Dequeue();
                    Console.WriteLine($"#{k + 2} player(kart: {second.TypeKart})");
                }
                if (first.TypeKart >= second.TypeKart)
                {
                    players[k].Karts.Enqueue(first);
                    players[k].Karts.Enqueue(second);
                    Console.WriteLine($"Take first player (з пари)");
                }
                else
                {
                    
                    if (k == players.Count - 1)
                    {
                        players[k].Karts.Enqueue(first);
                        players[k].Karts.Enqueue(second);
                    }
                    else
                    {
                        players[k + 1].Karts.Enqueue(first);
                        players[k + 1].Karts.Enqueue(second);
                    }
                    Console.WriteLine($"Take second player (з пари)");
                }

                for(int i = 0; i < players.Count; i++)
                {
                    if (players[i].Karts.Count == 0)
                        players.RemoveAt(i);
                }
                for (int i = 0; i < players.Count; i++)
                {
                    if (players[i].Karts.Count == countPlayer * cartsInPlayerOnStart)
                    {
                        Console.WriteLine($"Win {i+1} player");
                        k = -1;
                    }
                }
                if (k == -1)
                    break;
                k++;
                if (k >= players.Count)
                    k = 0;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Game game = new Game(2);
            game.GamePlay();
        }
    }
}
