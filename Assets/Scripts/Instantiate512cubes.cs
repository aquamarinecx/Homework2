using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instantiate512cubes : MonoBehaviour {

    // Prefab cube that gets spawned in.
	public GameObject cubePrefab;

	// Array that holds the 512 cubes we're spawning in.
	GameObject[] cubes = new GameObject[512];

    // Scales the height of each cube by this much.
	public float scale = 10000;

	/* Spawns 512 instances of cubePrefab in a circle of radius 100
     * around the object this script is attached to. Each cube is
     * rotated to face towards/away the center, and each cube is a
     * child of the object this script is attached to.
     */
	void Start () {
		for (int i = 0; i < 512; i++) {
            // Spawns a copy of cubePrefab.
            GameObject cube = Instantiate(cubePrefab);

			// Assigns this copy to it's proper position in cubes.
			cubes[i] = cube;

			// Names it properly.
			cube.name = "Cube" + i;

            // Set its parent to this object.
            cube.transform.parent = this.transform;

            /* Rotate and position the cube properly. Some attributes that
             * might come in handy include Transform.eulerAngles, Transform.forward,
             * and the Transform class in general. Make sure you're using floats
             * if you plan on doing any division.
             */

            float angle = i * 360 / 512f;

            cube.transform.Rotate(0, angle, 0);
            cube.transform.position = this.transform.position + cube.transform.forward * 100f;
        }
	}
	
	/* Every frame, we'll take the data collected in SpectrumAnalyzer
     * and use it to set the heights of our cubes. Each of the 512 data
     * points our sample array corresponds to one of our cubes. Two caveats:
     *     1. FFT values are very small, so you'll want to scale each one up
     *        (use the scale variable).
     *     2. If a FFT value is 0, you don't want the cube to disappear, so
     *        add a small base height to every cube.
     * Hint: You can access public static variables using Class.variable.
     */
	void Update () {
		for (int i = 0; i < 512; i++) {
			if (cubes != null) {
                GameObject cube = cubes[i];

                cube.transform.localScale = new Vector3(10, (SpectrumAnalyzer.samples[i] * scale) + 2, 10);
            }
		}
	}
}