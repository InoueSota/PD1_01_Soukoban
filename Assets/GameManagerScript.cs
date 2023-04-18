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
        // �ړ��悪�͈͊O�Ȃ�ړ��s��
        if(moveTo < 0 || moveTo >= map.GetLength(1)){ return false; }
        // �ړ����2(��)��������
        if(map[moveFrom, moveTo] == 2)
        {
            // �ǂ̕����ֈړ����邩���Z�o
            int velocity = moveTo - moveFrom;

            // �����ċA�ňړ��������ʂ�bool�ŋL�^
            bool success = MoveSideNumber(2, moveTo, moveTo + velocity);

            // �����ړ����s������A�v���C���[�̈ړ������s
            if(!success) { return false; }
        }
        map[moveFrom, moveTo] = number;
        map[moveFrom, moveFrom] = 0;
        return true;
    }

    bool MoveVerticalNumber(int number, int moveFrom, int moveTo)
    {
        // �ړ��悪�͈͊O�Ȃ�ړ��s��
        if (moveTo < 0 || moveTo >= map.GetLength(0)) { return false; }
        // �ړ����2(��)��������
        if (map[moveTo, moveFrom] == 2)
        {
            // �ǂ̕����ֈړ����邩���Z�o
            int velocity = moveTo - moveFrom;

            // �����ċA�ňړ��������ʂ�bool�ŋL�^
            bool success = MoveVerticalNumber(2, moveTo, moveTo + velocity);

            // �����ړ����s������A�v���C���[�̈ړ������s
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
        //�ړ���
        if (Input.GetKeyDown(KeyCode.LeftArrow) || 
            Input.GetKeyDown(KeyCode.RightArrow) ||
            Input.GetKeyDown(KeyCode.UpArrow) ||
            Input.GetKeyDown(KeyCode.DownArrow))
        {
            int playerIndex = GetPlayerIndex();

            //���ړ�
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveSideNumber(1, playerIndex, playerIndex - 1);
            }

            //�E�ړ�
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveSideNumber(1, playerIndex, playerIndex + 1);
            }

            //��ړ�
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveVerticalNumber(1, playerIndex, playerIndex - 1);
            }

            //���ړ�
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveVerticalNumber(1, playerIndex, playerIndex + 1);
            }

            PrintArray();
        }
    }
}
