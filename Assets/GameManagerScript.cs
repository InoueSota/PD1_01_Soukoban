using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    int[,] map;

    void PrintArray()
    {
        string debugText = "";
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (x == map.GetLength(1) - 1)
                {
                    debugText += map[y, x].ToString() + "\n";
                }
                else
                {
                    debugText += map[y,x].ToString() + ", ";
                }
            }
        }
        Debug.Log(debugText);
    }

    int GetPlayerIndex()
    {
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y,x] == 1)
                {
                    return y;
                }
            }
        }
        return -1;
    }

    bool MoveSideNumber(int number, int moveFrom, int moveTo)
    {
        // 移動先が範囲外なら移動不可
        if(moveTo < 0 || moveTo >= map.GetLength(1)){ return false; }
        // 移動先に2(箱)がいたら
        if(map[moveFrom, moveTo] == 2)
        {
            // どの方向へ移動するかを算出
            int velocity = moveTo - moveFrom;

            // 箱を再帰で移動させ結果をboolで記録
            bool success = MoveSideNumber(2, moveTo, moveTo + velocity);

            // 箱が移動失敗したら、プレイヤーの移動も失敗
            if(!success) { return false; }
        }
        map[moveFrom, moveTo] = number;
        map[moveFrom, moveFrom] = 0;
        return true;
    }

    bool MoveVerticalNumber(int number, int moveFrom, int moveTo)
    {
        // 移動先が範囲外なら移動不可
        if (moveTo < 0 || moveTo >= map.GetLength(0)) { return false; }
        // 移動先に2(箱)がいたら
        if (map[moveTo, moveFrom] == 2)
        {
            // どの方向へ移動するかを算出
            int velocity = moveTo - moveFrom;

            // 箱を再帰で移動させ結果をboolで記録
            bool success = MoveVerticalNumber(2, moveTo, moveTo + velocity);

            // 箱が移動失敗したら、プレイヤーの移動も失敗
            if (!success) { return false; }
        }
        map[moveTo, moveFrom] = number;
        map[moveFrom, moveFrom] = 0;
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        map = new int[,] {{ 0, 0, 0, 1, 0, 2, 0, 0, 0 },
                          { 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                          { 0, 0, 0, 0, 2, 0, 0, 0, 0 }};
        PrintArray();
    }

    // Update is called once per frame
    void Update()
    {
        //移動時
        if (Input.GetKeyDown(KeyCode.LeftArrow) || 
            Input.GetKeyDown(KeyCode.RightArrow) ||
            Input.GetKeyDown(KeyCode.UpArrow) ||
            Input.GetKeyDown(KeyCode.DownArrow))
        {
            int playerIndex = GetPlayerIndex();

            //左移動
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveSideNumber(1, playerIndex, playerIndex - 1);
            }

            //右移動
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveSideNumber(1, playerIndex, playerIndex + 1);
            }

            //上移動
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveVerticalNumber(1, playerIndex, playerIndex - 1);
            }

            //下移動
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveVerticalNumber(1, playerIndex, playerIndex + 1);
            }

            PrintArray();
        }
    }
}
