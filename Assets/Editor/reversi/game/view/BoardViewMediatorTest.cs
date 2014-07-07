using NUnit.Framework;
using System;
using NSubstitute;
using UnityEngine;

namespace reversi.game
{
	[TestFixture]
	public class BoardViewMediatorTest
	{
		BoardViewMediator _instance;
		GameObject go;
		BoardView view;
		PiecePlacedSignal piecePlacedSignal;

		[SetUp]
		public void SetUp()
		{
			view = Substitute.For<BoardView>();
			piecePlacedSignal = Substitute.For<PiecePlacedSignal>();

			go = GameObject.CreatePrimitive(PrimitiveType.Cube);
			_instance = go.AddComponent<BoardViewMediator>();
			_instance.View = view;
			_instance.PiecePlaced = piecePlacedSignal;
		}

		[TearDown]
		public void TearDown()
		{
			_instance = null;
			GameObject.DestroyImmediate(go);
		}

		[Test]
		public void OnRegisterAddsSignalListener()
		{
			_instance.OnRegister();

			piecePlacedSignal.Received().AddListener(_instance.HandlePiecePlaced);
		}

		[Test]
		public void OnRemoveRemovesSignalListener()
		{
			_instance.OnRemove();

			piecePlacedSignal.Received().RemoveListener(_instance.HandlePiecePlaced);
		}
	}
}