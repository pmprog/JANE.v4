
#pragma once

#include "../includes.h"

class Panel
{

  public:
    int ObjectGraphicIndex;
    int ColourRemap[16];
    int ScreenX;
    int ScreenY;
    bool FlipHorizontal;
    bool FlipVertical;
    int BackgroundAtY;

    Panel();

    static bool SortingPredicate(const Panel* A, const Panel* B);

    void Load(ConfigFile* DataFile, std::string KeyPrefix);
    void Save(ConfigFile* DataFile, std::string KeyPrefix);

    void Load(SQLiteDB* Database, int GameID, int RoomID, int PanelID);
    void Save(SQLiteDB* Database, int GameID, int RoomID, int PanelID);

};
