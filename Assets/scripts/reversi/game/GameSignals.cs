using strange.extensions.signal.impl;
using reversi.game;

public class GameContextStartSignal : Signal {}
public class SetInitialStateSignal : Signal {}
public class PlacePieceSignal : Signal<GridCellKey, Faction> {}
public class PiecePlacedSignal : Signal<GridCellKey, Faction> {}