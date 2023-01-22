// This is a JavaScript module that is loaded on demand. It can export any number of
// functions, and may import other JavaScript modules if required.
import "https://gustavgenberg.github.io/handy-front-end/Pointer.js";
//import "https://gustavgenberg.github.io/handy-front-end/Canvas.js";
export function showPrompt(message) {
  return prompt(message, "Type anything here");
}



const canvas = document.getElementById("image-canvas");

const ctx = canvas.getContext("2d");


canvas.width = 500;
canvas.height = 500;

let isPainting = false;

let startImage;
const mouse = new Pointer();
let squares = [];

export function drawImage(image) {
    startImage = image;
    
    ctx.drawImage(image, 0, 0, 500, 500);
    canvas.addEventListener("mousedown", () => {
        isPainting = true;
    });
    canvas.addEventListener("mouseout", () => {
        isPainting = false;
    });

    canvas.addEventListener("mousemove", () => {
        
        //startX = e.clientX - canvasOffsetX;
        //startY = e.clientY - canvasOffsetY;
        
        if (isPainting) {
            if (mouse.isDown(mouse.BTN_LEFT)) {
                //ctx.clearRect(startX, startY, rectW, rectH);
                //ctx.clear();
                ctx.drawImage(image, 0, 0, 500, 500);
                squares.forEach(function(square) {

                    ctx.clearRect(square.x, square.y, square.w, square.h);

                });
                const relativeStart = mouse.relative(canvas,
                    mouse.mousedown[mouse.BTN_LEFT].x,
                    mouse.mousedown[mouse.BTN_LEFT].y);
                const relativeCurrent = mouse.relative(canvas, mouse.x, mouse.y);

                const width = (relativeCurrent.x - relativeStart.x);
                const height = (relativeCurrent.y - relativeStart.y);

                ctx.clearRect(relativeStart.x, relativeStart.y, width, height);

            }
        }
       
    });
   
    mouse.on("up", function (event) {

        if (event.button === mouse.BTN_LEFT && isPainting) {

            const entry = mouse.getLastClick(mouse.BTN_LEFT);

            const relative = mouse.relative(canvas, entry.x, entry.y);
            const endrelative = mouse.relative(canvas, entry.endx, entry.endy);
            const specs = {
                Entry: entry,
                Relative: relative,
                EndRelative: endrelative,
                Event: event
            }
            
            const square = {

                x: Math.min(relative.x, endrelative.x),
                y: Math.min(relative.y, endrelative.y),
                w: Math.max((relative.x - endrelative.x), (endrelative.x - relative.x)),
                h: Math.max((relative.y - endrelative.y), (endrelative.y - relative.y))

            };
            console.log(`Square created: ${JSON.stringify(square)}\n Using specs: ${JSON.stringify(specs, "\t")}`);
            squares.push(square);

        }

    });
}


export function clear() {
    ctx.clearRect(0, 0, 500, 500);
    ctx.drawImage(startImage, 0, 0, 500, 500);
    squares = [];
}
export function grabImage() {
    const result = [];
    squares.forEach(function (square) {

        const rect = {
            X: square.x,
            Y: square.y,
            Width: square.w,
            Height: square.h
        };
        result.push(rect);

    });
    return result;
}
export function removeLast() {
    const removed = squares.pop();
    console.log(`From JS\nRemoved square: ${JSON.stringify(removed)}`);
    ctx.clearRect(0, 0, 500, 500);
    ctx.drawImage(startImage, 0, 0, 500, 500);
    squares.forEach(function (square) {

        ctx.clearRect(square.x, square.y, square.w, square.h);

    });
    
}


