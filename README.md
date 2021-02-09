# 2021-02-09-GizmoSceneEnhancer
 
I started by looking on the web how to do a custom editor windows, official Unity website was a really great help but I also find some answer on forum like StackOverflow.

Next step was to load data, when the user click on the Button " open " from the inspector, the windows open and the data are loaded however when the user go to Windows > Custom > Scene Enhancer, the data are not loaded, it make sens for now because, when the user push " Open" the data are passed throught a parameter of the right functions.
I have to find a ways to load the data independantly.

Then I start looking how to spawn the object in the scene, and I spend a lot of time trying to find a solution that work.
I finally understand how to do by I'm unsatisfied with my actual answer because the function OnSceneGui that allow me to draw a sphere is called too many time because of the source parameter, I'll try to find a proper solution to fix it later.

This is it for my first day, it wasn't easy, i spend a lot of time with Google but I really learn something new.

To conclude, I would say I made discutable choices but they allow me to keep going, I'll fix later if time allows me.
