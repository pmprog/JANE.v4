
#pragma once

class Panel
{

  public:
    int ObjectGraphicIndex;
    int ColourRemap[16];
    int ScreenX;
    int ScreenY;
    bool FlipHorizontal;
    bool FlipVertical;
    int ForegroundAtY;

    Panel();

    bool operator<(const Panel& cmp) const;
    bool operator<(const Panel* cmp) const;

    static bool SortingPredicate(const Panel* A, const Panel* B);
};
