using UnityEngine;
using System.Collections;
using strange.extensions.context.impl;
using strange.extensions.signal.impl;
using strange.extensions.command.impl;
using reversi.game.space;

namespace reversi.game
{
	public class GameStartupCommand : Command
	{
		[Inject]
		public IResources resources { get; set; }

		[Inject]
		public IGameObject gameObject { get; set; }

		[Inject]
		public CreateGridSpacesSignal spaceSignals { get; set; }

		override public void Execute()
		{
			buildGridSpaces( 4, 4 );

			addSceneLighting();
			demoPopulatedScene();
		}

		void buildGridSpaces ( int columns, int rows )
		{
			spaceSignals.Dispatch("Grid", new Vector2( 4, 4 ));
		}

		void addSceneLighting ()
		{
			GameObject lightGameObject = new GameObject("Key Light");
			lightGameObject.AddComponent<Light>();
			lightGameObject.light.type = LightType.Directional;
			lightGameObject.transform.Rotate ( new Vector3( 30f, 30f, 0) );
			lightGameObject.light.color = Color.white;
			lightGameObject.light.intensity = 0.4f;
		}

		void demoPopulatedScene ()
		{
			GameObject diskPrefab = resources.Load<GameObject>("Prefabs/reversi_piece");
			for (int row = 0; row < 4; row++)
			{
				for (int col = 0; col < 4; col++)
				{
					GameObject cur = (GameObject)gameObject.Instantiate(diskPrefab);
					cur.transform.position = new Vector3( 1f * col, 1f * row, 0f);
					cur.transform.Rotate( Vector3.right, 90 );
				}
			}
		}
	}
}