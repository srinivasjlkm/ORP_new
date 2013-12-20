    var showingMouse : boolean;//for checking if mouse is shown or not
 
    function Start(){
    showingMouse = false;
    Screen.showCursor = false;
    Screen.lockCursor = true;
    }
 
//executed every frame
    function Update(){
 
    //if you press "r"  and the mouse is not shown then the mouse is shown and movable then execute code
    if(Input.GetKeyDown(KeyCode.R) && showingMouse == false){
    showingMouse = true;
    Screen.showCursor =true;
    Screen.lockCursor = false;
    }
    //if you press "r" and the mouse is shown then make it not shown
    else if(Input.GetKeyDown(KeyCode.R) && showingMouse == true){
    showingMouse = false;
    Screen.showCursor =false;
    Screen.lockCursor = true;
    }
 
    }