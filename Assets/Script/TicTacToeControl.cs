using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToeCopControl : MonoBehaviour
{
    public int turn;
    public int count; //for checking draw?
    public int players = 2;
    private int[,] cells = new int[3, 3]; // save gird's info

    void Start()
    {
        restart();
    }

    void restart() // reset
    {
        
        for (int i = 0; i < 3; ++i)
        {
            for (int j = 0; j < 3; ++j)
            {
                cells[i, j] = 0;
            }
        }
        turn = 1;
        count = 0;
    }

    private int winCheck()
    {
        for (int row = 0; row < 3; ++row)
        {
            if (cells[row, 0] != 0 && cells[row, 0] == cells[row, 1] && cells[row, 1] == cells[row, 2])
            {
                return cells[row, 0];
            }
        }

        for (int col = 0; col < 3; ++col)
        {
            if (cells[0, col] != 0 && cells[0, col] == cells[1, col] && cells[1, col] == cells[2, col])
            {
                return cells[0, col];
            }
        }

        if (cells[1, 1] != 0 && cells[0, 0] == cells[1, 1] && cells[1, 1] == cells[2, 2] || cells[0, 2] == cells[1, 1] && cells[1, 1] == cells[2, 0])
        {
            return cells[1, 1];
        }
        if (count >= 9)
            return 3;

        return 0;
    }

    int AIthink(int x, int y)
    {

        // cross check
        if (x == y)
        {
            if (cells[(x + 1) % 3, (y + 1) % 3] == 1 && cells[(x + 2) % 3, (y + 2) % 3] == 1)
                return 1;
            if (cells[(x + 1) % 3, (y + 1) % 3] == 2 && cells[(x + 2) % 3, (y + 2) % 3] == 2)
                return 2;
        }

        if (x + y == 2)
        {
            if (cells[(x + 1) % 3, (y + 2) % 3] == 1 && cells[(x + 2) % 3, (y + 1) % 3] == 1)
                return 1;
            if (cells[(x + 1) % 3, (y + 2) % 3] == 2 && cells[(x + 2) % 3, (y + 1) % 3] == 2)
                return 2;
        }

        
        // vertical and horizantol check
        if (cells[(x + 1) % 3, y] == 1 && cells[(x + 2) % 3, y] == 1)
            return 1;
        if (cells[x, (y + 1) % 3] == 1 && cells[x, (y + 2) % 3] == 1)
            return 1;

        if (cells[(x + 1) % 3, y] == 2 && cells[(x + 2) % 3, y] == 2)
            return 2;
        if (cells[x, (y + 1) % 3] == 2 && cells[x, (y + 2) % 3] == 2)
            return 2;

        return 0;
    }




    private void OnGUI()
    {
        GUIStyle temp1 = new GUIStyle
        {
            fontSize = 60
        };
        temp1.normal.textColor = Color.black;
        temp1.fontStyle = FontStyle.Bold;

        GUIStyle temp2 = new GUIStyle
        {
            fontSize = 30
        };
        temp2.normal.textColor = Color.red;
        temp2.fontStyle = FontStyle.Italic;


        GUI.Label(new Rect(350, 20, 100, 50), "TicTacToe", style: temp1);
        if (GUI.Button(new Rect(700, 130, 100, 50), "restart"))
        {
            restart();
        }

        
        if (GUI.Button(new Rect(700, 50, 150, 60), ""))
        {
            players = 3 - players;
            restart();
        }
        if (players == 2)
        {
            GUI.Label(new Rect(720, 65, 150, 60), " Player vs Player");
        }
        if (players == 1)
        {
            GUI.Label(new Rect(720, 65, 150, 60), " Player vs AI");
        }

        int result = winCheck();//
       
        switch (result)
        {
            case 1:
                GUI.Label(new Rect(420, 90, 100, 50), "X WIN", style: temp2);
                break;
            case 2:
                GUI.Label(new Rect(420, 90, 100, 50), "O WIN", style: temp2);
                break;
            case 3:
                GUI.Label(new Rect(420, 90, 100, 50), "DUAL", style: temp2);
                break;
        }

        
        if (players == 1 && turn == -1)
        {
            int flag = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (cells[i, j] == 0)
                    {
                        int r = AIthink(i, j);
                        if (r != 0)
                        {
                            cells[i, j] = 2;
                            i = 3;
                            j = 3;
                            flag = 1;
                        }
                    }

                }
            }
            if (flag == 0)
            {
                if (cells[1, 1] == 0)
                {
                    cells[1, 1] = 2;
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                        for (int j = 0; j < 3; j++)
                        {
                            if (cells[i, j] == 0)
                            {
                                cells[i, j] = 2;
                                i = 3;
                                j = 3;
                            }
                        }
                }
            }
            turn = 1;
            count++;
        }

        for (int i = 0; i < 3; ++i)
        {
            for (int j = 0; j < 3; ++j)
            {
                if (cells[i, j] == 1)
                {
                    GUI.Button(new Rect(240 + i * 120, 150 + j * 120, 120, 120), "X");
                }
                if (cells[i, j] == 2)
                {
                    GUI.Button(new Rect(240 + i * 120, 150 + j * 120, 120, 120), "O");
                }
                if (GUI.Button(new Rect(240 + i * 120, 150 + j * 120, 120, 120), ""))
                {
                    if (result == 0)
                    {
                        if (turn == 1) cells[i, j] = 1;
                        else cells[i, j] = 2;
                        count++;
                        turn = -turn;
                    }
                }
            }
        }
    }

}