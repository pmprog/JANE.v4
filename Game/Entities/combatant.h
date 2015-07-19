
#pragma once

#include "../includes.h"


class Combatant
{

  public:
    std::string SkinFilename;
		int ColourRemap[16];

    Combatant();
    ~Combatant();

    void Load(ConfigFile* DataFile, std::string KeyPrefix);
    void Save(ConfigFile* DataFile, std::string KeyPrefix);

};
