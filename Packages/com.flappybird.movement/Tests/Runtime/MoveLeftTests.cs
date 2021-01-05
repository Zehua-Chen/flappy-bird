using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

namespace FlappyBird.Movement.Tests
{
    public class MoveLeftTests
    {
        [UnityTest]
        public IEnumerator Test()
        {
            yield return null;
            //var gameObject = new GameObject();
            //gameObject.transform.position = new Vector3();
            //gameObject.name = "Test Move Left";

            //var rig = gameObject.AddComponent<Rigidbody2D>();
            //rig.bodyType = RigidbodyType2D.Kinematic;

            //var settings = ScriptableObject.CreateInstance<SpeedSettings>();

            //var moveLeft = gameObject.AddComponent<MoveLeft>();
            //moveLeft.SpeedSettings = settings;
            //moveLeft.Rigidbody2D = rig;

            //yield return new WaitForSeconds(1.0f);

            //Assert.Less(gameObject.transform.position.x, 0.0f);
            //Assert.True(Mathf.Approximately(0.0f, gameObject.transform.position.y));
            //Assert.True(Mathf.Approximately(0.0f, gameObject.transform.position.z));
        }
    }
}
