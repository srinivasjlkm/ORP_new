var myCursor:Texture2D;
var cursorSizeX: int = 32;  // set to width of your cursor texture
var cursorSizeY: int = 32;  // set to height of your cursor texture
var condition = true;
 
function OnMouseEnter(){
    condition = false;
    Screen.showCursor = false;
}
 
function OnMouseExit(){
    condition = true;
    Screen.showCursor = true;
}
 
function OnGUI(){
    if(!condition){
       GUI.DrawTexture (Rect(Input.mousePosition.x-cursorSizeX/2 + cursorSizeX/2, (Screen.height-Input.mousePosition.y)-cursorSizeY/2 + cursorSizeY/2, cursorSizeX, cursorSizeY),myCursor);
    }
}