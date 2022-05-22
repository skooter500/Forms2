# Game Engines 2 2022 - Lab Test

## Rules

- Please stay on teams for the duration of the test. 
- You have 3 hours to complete the test
- You can access the [Unity Reference](https://docs.oracle.com/javase/7/docs/api/) and the [Git reference](https://git-scm.com/docs) if you need to look something up
- No use of notes or previously written code
- No collaboration or communication

Nematodes, also called roundworms, are multicellular organisms that occur as parasites in animals and plants or as free-living forms in soil and water. Many species of nematodes are microscopic, however, some species such as tapeworms can grow to several meters in length. They are the most abundant multicellular lifeforms on the planet and there are over 400 quintillion individual nematodes on earth. 

In today's lab test you will be coding elements of this Nematode simulation (click the images for videos):

[![YouTube](http://img.youtube.com/vi/62rLDaMyFSo/0.jpg)](https://youtu.be/62rLDaMyFSo)



[![YouTube](http://img.youtube.com/vi/869m0ROXjJ4/0.jpg)](https://youtu.be/869m0ROXjJ4)

## Instructions

- Fork this repo. It contains all the code and examples we made this semester.
- Set up origin and upstream remotes on your fork.
- Open the nematodes scene in Unity and open the project in VS Code.
- Inspect the nematode gameObject. This gameObject is the prefab for the school. There is a script attached called Nematode.cs. The code in Awake has been removed. You should write code to create the Nematode segments according to the following design:
    - The segments should be made from spheres positioned one unit apart, behind each other.     
    - The positions and rotations should be relative to the nematode gameobject the script is attached to.
    - The width and height of the segments tapers towards the ends. I suggest you get other parts working before working on this.
    - A random value can be assigned to the Length field and this controls how many segments are created. 
    - Segments should be coloured as per the video.
    - Also, the segments need to be parented to the nematode gameobject for the SpineAnimator to work.
    - Assign a Boid to the front segment. The following behaviours can be added:     
        - Obstacle Avoidance
        - Constrain
        - Noise Wander with a vertical axis set
        - Noise Wander with a horizontal axis set
        - Set appropriate weights, priorities and fields on the behaviours to achieve desired results
        - I suggest you get wandering working first, then the rest 
    - The school game object in the scene has a script attached that creates the school of nematodes randomly positioned in a sphere and with random rotations around the Y axis. the code in this script has been removed.

- [Submit your solution](https://forms.office.com/Pages/ResponsePage.aspx?id=yxdjdkjpX06M7Nq8ji_V2ou3qmFXqEdGlmiD1Myl3gNUN1VSNUtaRjJNM1czTFJOSjZLTTgxOVFQWC4u)

## Marking Scheme

| Marks | Description |
|-------|-------------|
| 30 | Nematode segments  |
| 20 | Nematode behaviours |
| 20 | Nematode school |
| 20 | Any other cool thing |
| 10 | Correct use of git |