
namespace BaseFramework
{
public class Player
{
    public string name;
    public int[]evenNum; //for Tic-tac-toe
    public int[]oddNum; //for Tic-tac-toe
    public bool isOdd=true;

    public Player( string name, bool isOdd)
    {
        IsOdd = isOdd;
        Name= name;


    }
    public string Name{
        get{ return name;}
        set{ name=value;}
    }
    public bool IsOdd{
        get { return isOdd; }
        set { isOdd = value;}
    }
    
    //for Tic-tac-toe
    public void AssignNumbers(int maxNum, int boardSize)
        {
            maxNum = boardSize * boardSize;
            if (isOdd)
            {
                oddNum = new int[(maxNum + 1) / 2];

            }

            else
            {
                evenNum = new int[maxNum / 2];

            }
            int index = 0;

            for (int i = 1; i <= maxNum; i++)
            {
                if (isOdd && i % 2 == 1)
                {
                    oddNum[index] = i;
                    index++;
                }
                else if (!isOdd && i % 2 == 0)
                {
                    evenNum[index] = i;
                    index++;
                }
            }


        }

    public virtual string GetInput(IBoard board)
    {
        Console.WriteLine(name+ " Enter your move: ");
        return Console.ReadLine();
    }

    }
}