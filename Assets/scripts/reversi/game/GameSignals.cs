using strange.extensions.signal.impl;
using reversi.game;

public class GameContextStartSignal : TestableSignal {}
public class InitializeBoardSignal : TestableSignal<GridCellKey> {}
public class SetInitialStateSignal : TestableSignal {}
public class PlacePieceSignal : TestableSignal<GridCellKey, Faction> {}
public class PiecePlacedSignal : TestableSignal<GridCellKey, Faction> {}