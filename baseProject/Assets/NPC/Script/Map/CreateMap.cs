using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ダンジョンの生成を行うスクリプト
/// 追加/改造できそうな項目
/// ・親オブジェクトを生成して各オブジェクトごとに纏める
/// ・アイテムの生成
/// ・通路の横幅をランダムで2マスにする(同じ通路内で)：人口感をなくすため
/// ・ゴール、スタート位置の生成
/// </summary>
public class CreateMap : MonoBehaviour
{
    public class DviRoomInformation
    {
        public int top = 0; 
        public int left = 0;
        public int bottom = 0;
        public int right = 0;
        public int areaRank = 0;
        public int nextRoom = 0;
        public bool isNext = false;
        public int nextRoomPos = 0;
    }
    [Tooltip("床のオブジェクト"),SerializeField] private GameObject floor;
    [Tooltip("壁のオブジェクト"),SerializeField] private GameObject wall;
    [Tooltip("外壁のオブジェクト"),SerializeField] private GameObject outerWall;
    [Tooltip("プレイヤーのトランスフォーム"),SerializeField] private GameObject playerObj;
    [Tooltip("プレイヤーを映しておくカメラのトランスフォーム"),SerializeField] private Transform cameraTransform;
    [Tooltip("カメラの高さ"),SerializeField] private float cameraHeight;
    [Tooltip("マップ全体の大きさ"),SerializeField,Range(20,100)] private int mapWidth = 50;
    [Tooltip("マップ全体の高さ"),SerializeField,Range(0,100)] private int mapHeight = 30;
    [Tooltip("壁の高さ"),SerializeField] private int wallHeight = 2;
    [Tooltip("床と壁の間の幅"),SerializeField,Range(0,3)] private float widthFloorAndWall = 0.1f;
    [Tooltip("プレイヤーの初期の高さ"),SerializeField] private float firstPlayerHeight = 1.0f;
    
    private const int wallID = 0;
    private const int roomID = 1;
    private const int roadID = 2;
    private int[,] mapArray;

    [Header("部屋の数Min,Max(最小,最大)")]
    [SerializeField,Range(1,10)] private int minRooms = 1;
    [SerializeField,Range(1,20)] private int maxRooms = 13;
    private int roomNum;
    private Vector3 firstPlayerPosition;

    private List<DviRoomInformation> roomDvi = new List<DviRoomInformation>();

    private void Start()
    {
        // マップの壁を生成
        MapWallData();
        // マップの分割
        MapDivision();
        // 部屋を生成
        RoomCreate();
        // 道を生成
        RoadCreate();
        // ダンジョン生成
        CreateDungeon();
        // ダンジョン初期設定
        InitDungeon();
        
    }

    /// <summary>
    /// マップの壁(壁しかない状態のもの)を生成
    /// </summary>
    private void MapWallData()
    {
        mapArray = new int[mapWidth,mapHeight];  // Mapデータ[縦、横]

        // 設定した縦横分の壁を敷き詰める
        for(int i = 0;i<mapWidth; ++i)
        {
            for(int j = 0;j<mapHeight;++j)
            {
                mapArray[i,j] = wallID;
            }
        }
    }
    /// <summary>
    /// 設定したマップの大きさに合わせた床を設置する関数
    /// </summary>
    private void MapFloorData()
    {
        for(int i = 0;i<mapWidth;++i)
        {
            for(int j = 0; j<mapHeight;++j)
            {
                //床を敷き詰める
                Instantiate(floor,new Vector3(i - mapWidth / 2 , 0 , j - mapHeight / 2),Quaternion.identity);
                if(mapArray[i,j]==wallID)
                {
                    for(int height = 0;height < wallHeight;++height)
                    {
                        Instantiate(wall,new Vector3(i - mapWidth / 2 , height + 1 , j - mapHeight / 2),Quaternion.identity);
                    }
                }
            }
        }
    } 

    /// <summary>
    /// 部屋を作るためにマップを分割する関数
    /// </summary>
    private void MapDivision()
    {
        int divisionPos;
        roomNum = Random.Range(minRooms,maxRooms);
        roomDvi.Add(new DviRoomInformation());
        roomDvi[0].top = 0;
        roomDvi[0].left = 0;
        roomDvi[0].bottom = mapHeight - 1;
        roomDvi[0].right = mapWidth - 1;
        roomDvi[0].areaRank = roomDvi[0].bottom + roomDvi[0].right;

        for(int i = 1; i < roomNum; ++i)
        {
            roomDvi.Add(new DviRoomInformation());
            int target = 0;
            int areaMax = 0;
            // 最大面積を持つ区画の指定
            for(int j = 0; j < i;++j)
            {
                if(roomDvi[j].areaRank >= areaMax)
                {
                    areaMax = roomDvi[j].areaRank;
                    target = j;
                }
            }

            // 分割点をもとめる
            if(roomDvi[target].bottom - roomDvi[target].top > 12 && roomDvi[target].right - roomDvi[target].left > 12)
            {
                roomDvi[i].nextRoom = target;
                divisionPos = Random.Range(0,roomDvi[target].areaRank);

                if(divisionPos > roomDvi[target].bottom - roomDvi[target].top)
                {
                    roomDvi[i].left = roomDvi[target].left + Random.Range(6, roomDvi[target].right - roomDvi[target].left - 6);
                    roomDvi[i].right = roomDvi[target].right;
                    roomDvi[target].right = roomDvi[i].left - 1;
                    roomDvi[i].top = roomDvi[target].top;
                    roomDvi[i].bottom = roomDvi[target].bottom;
                    roomDvi[i].isNext = true;
                    roomDvi[i].nextRoomPos = roomDvi[i].left;
                }   
                else
                {
                    roomDvi[i].top = roomDvi[target].top + Random.Range(6, roomDvi[target].bottom - roomDvi[target].top - 6);
                    roomDvi[i].bottom = roomDvi[target].bottom;
                    roomDvi[target].bottom = roomDvi[i].top - 1;
                    roomDvi[i].left = roomDvi[target].left;
                    roomDvi[i].right = roomDvi[target].right;
                    roomDvi[i].isNext = false;
                    roomDvi[i].nextRoomPos= roomDvi[i].top;
                }
                roomDvi[i].areaRank = roomDvi[i].bottom - roomDvi[i].top + roomDvi[i].right - roomDvi[i].left;
                roomDvi[target].areaRank = roomDvi[target].bottom - roomDvi[target].top + roomDvi[target].right - roomDvi[target].left;

            }
            else
            {
                roomNum = i;
                break;
            }
        }
    }

    /// <summary>
    /// 部屋を作成する関数
    /// </summary>
    private void RoomCreate()
    {
        int diffX,diffY;
        int moveX,moveY;
        for(int i = 0; i < roomNum; ++i)
        {
            if(roomDvi[i] != null)
            {
                diffX = roomDvi[i].right - roomDvi[i].left;
                diffX = Mathf.FloorToInt(Random.Range(0.500f,0.800f) * diffX);
                diffY = roomDvi[i].bottom - roomDvi[i].top;
                diffY = Mathf.FloorToInt(Random.Range(0.500f,0.800f) * diffY);
                moveX = Mathf.FloorToInt((roomDvi[i].right - roomDvi[i].left - diffX) / 2.0f);
                moveY = Mathf.FloorToInt((roomDvi[i].bottom - roomDvi[i].top - diffY) / 2.0f);
                
                
                roomDvi[i].left = roomDvi[i].left + moveX;
                roomDvi[i].right = roomDvi[i].left + diffX;
                roomDvi[i].top = roomDvi[i].top + moveY;
                roomDvi[i].bottom = roomDvi[i].top + diffY;

                for(int j = 0; j < diffY; ++j)
                {
                    for(int k = 0 ; k < diffX ; ++k)
                    {
                        mapArray[roomDvi[i].left + k + 1,roomDvi[i].top + j + 1] = roomID;
                    }
                }
            }
            else
                break;
        }

        
    }

    /// <summary>
    /// 道を生成する関数
    /// </summary>
    private void RoadCreate()
    {
        int nowPos = 0;
        int nowDis = 0;
        int nextPos = 0;
        int nextDis = 0;
        
        for(int i = 1; i < roomNum; ++i)
        {
            if(roomDvi[i].isNext)
            {
                // 通路の開始地点、および終了地点を求める
                nowPos = roomDvi[i].bottom - roomDvi[i].top;
                nowPos = Random.Range(1,nowPos) + roomDvi[i].top;
                nextPos = roomDvi[roomDvi[i].nextRoom].bottom - roomDvi[roomDvi[i].nextRoom].top;
                nextPos = Random.Range(1,nextPos) + roomDvi[roomDvi[i].nextRoom].top;

                // 通路を引く線の長さ(開始、終了地点共に)求める
                nowDis = roomDvi[i].left - roomDvi[i].nextRoomPos + 1;
                nextDis = roomDvi[i].nextRoomPos - roomDvi[roomDvi[i].nextRoom].right + 1;

                // ライン作成
                for(int j = 0; j < nowDis ; ++j)
                {
                    mapArray[-j + roomDvi[i].left,nowPos] = roadID;
                }
                
                // ライン作成
                for(int j = 0; j < nextDis ; ++j)
                {
                    mapArray[j + roomDvi[roomDvi[i].nextRoom].right,nextPos] = roadID;
                }

                // 縦ライン作成
                for(int j = 0; ; ++j)
                {
                    //nowとnextのどちらが高いかを調べ、縦ラインを作成
                    if(nowPos >= nextPos)
                    {
                        if(nextPos + j < nowPos)
                        {
                            mapArray[roomDvi[i].nextRoomPos,nextPos + j] = roadID;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        if(nowPos + j < nextPos)
                        {
                            mapArray[roomDvi[i].nextRoomPos,nowPos + j] = roadID;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                // 通路の開始地点、および終了地点を求める。
                nowPos = roomDvi[i].right - roomDvi[i].left;
                nowPos = Random.Range(i,nowPos) + roomDvi[i].left;
                nextPos = roomDvi[roomDvi[i].nextRoom].right - roomDvi[roomDvi[i].nextRoom].left;
                nextPos = Random.Range(1,nextPos) + roomDvi[roomDvi[i].nextRoom].left;

                // 通路を引く線の長さ（開始、終了地点共に）求める
                nowDis = roomDvi[i].top - roomDvi[i].nextRoomPos + 1;
                nextDis = roomDvi[i].nextRoomPos - roomDvi[roomDvi[i].nextRoom].bottom + 1;

                // ライン作成
                for(int j = 0; j < nowDis; ++j)
                {
                    mapArray[nowPos, -j + roomDvi[i].top] = roadID;
                }
                for(int j = 0; j < nextDis; ++j)
                {
                    mapArray[nextPos, j + roomDvi[roomDvi[i].nextRoom].bottom] = roadID;
                }
                // 横ライン作成
                for(int j = 0; ; ++j)
                {
                    if(nowPos >= nextPos)
                    {
                        if(nextPos + j < nowPos)
                        {
                            mapArray[nextPos + j, roomDvi[i].nextRoomPos] = roadID;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        if(nowPos + j < nextPos)
                        {
                            mapArray[nowPos + j, roomDvi[i].nextRoomPos] = roadID;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// ダンジョンに必要なオブジェクトの生成
    /// </summary>
    private void CreateDungeon()
    {
        for(int i = 0; i < mapWidth; ++i)
        {
            for(int j = 0; j < mapHeight; ++j)
            {
                // 床のオブジェクトを生成
                Instantiate(floor,new Vector3(i - mapWidth / 2.0f, 0, j - mapHeight / 2.0f),Quaternion.identity);
                //　壁のオブジェクトを生成
                if(mapArray[i,j] == wallID)
                {
                    for(int height = 0; height < wallHeight; ++height)
                    {
                        Instantiate(wall,new Vector3(i - mapWidth / 2.0f,height + widthFloorAndWall , j - mapHeight / 2.0f),Quaternion.identity);
                    }
                }
            }
        }

        // 外壁部分を作る
        for(int i = - 1; i < mapHeight; ++i)
        {
            for(int j = 0; j < wallHeight; ++j)
            {   
                Instantiate(outerWall, new Vector3(-1 - mapWidth / 2.0f, j, i - mapHeight / 2.0f), Quaternion.identity);
                Instantiate(outerWall, new Vector3(mapWidth - mapWidth / 2.0f, j, i - mapHeight / 2.0f), Quaternion.identity);
            }
        }
        for(int i = - 1; i < mapWidth; ++i)
        {
            for(int j = 0; j < wallHeight; ++j)
            {   
                Instantiate(outerWall, new Vector3(i - mapWidth / 2.0f, j, -1.0f - mapHeight / 2.0f), Quaternion.identity);
                Instantiate(outerWall, new Vector3(i - mapWidth / 2.0f, j, mapHeight - mapHeight / 2.0f), Quaternion.identity);
            }
        }
    }

    /// <summary>
    /// ダンジョンの初期設定
    /// </summary>
    private void InitDungeon()
    {
        int randomRoom = Random.Range(0,roomNum);
        int x = Random.Range(0, roomDvi[randomRoom].right - roomDvi[randomRoom].left) + roomDvi[randomRoom].left;
        int z = Random.Range(0, roomDvi[randomRoom].bottom - roomDvi[randomRoom].left) + roomDvi[randomRoom].left;
        // playerの初期座標設定
        Instantiate(playerObj,new Vector3(x - mapWidth / 2 + 1, firstPlayerHeight,z - mapHeight / 2 + 1),Quaternion.identity);
        //playerObj.transform.position = new Vector3(x - mapWidth / 2 + 1, 0.5f,z - mapHeight / 2 + 1);
        Debug.Log("プレイヤーのポジション : " + playerObj.transform.position);
        // cameraの初期座標設定
        cameraTransform.position = new Vector3(playerObj.transform.position.x,cameraHeight,playerObj.transform.position.z);
        Debug.Log("カメラのポジション : " + cameraTransform.position);
        // カメラの角度
        cameraTransform.transform.rotation = Quaternion.Euler(new Vector3(90,0,0));
    }
}
