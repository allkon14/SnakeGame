// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const canvas = document.getElementById("game")
const ctx = canvas.getContext("2d")

const background = new Image();
background.src = "https://localhost:44390/img/background.jpg";

const apple_img = new Image(); // Создание объекта
apple_img.src = "https://localhost:44390/img/icon_apple.png"; // Указываем нужное изображение

const treat_img = new Image(); // Создание объекта
treat_img.src = "https://localhost:44390/img/icon_treat.png"; // Указываем нужное изображение

localStorage.clear();

let step = 48;
let score = 0;

let food = {
    x: Math.floor(Math.random() * 25 + 1) * step, // формирует значение от 1 до 25(кол-во элементов в сторке фон)
    y: Math.floor(Math.random() * 12 + 3) * step,
};

let treat = {
    x: Math.floor(Math.random() * 25 + 1) * step, // формирует значение от 1 до 25(кол-во элементов в сторке фон)
    y: Math.floor(Math.random() * 12 + 3) * step,
};

let snake = [];
snake[0] = {
    ////// x: 20 * step,
    ////// y: 10 * step
    x: Math.floor(Math.random() * 25 + 1) * step, // формирует значение от 1 до 25(кол-во элементов в сторке фон)
    y: Math.floor(Math.random() * 12 + 3) * step,
};

document.addEventListener("keydown", direction);
let dir;

function direction(event) {
    if ((event.keyCode == 37 || event.keyCode == 65) && dir != "right")
        dir = "left";
    else if ((event.keyCode == 38 || event.keyCode == 87) && dir != "down")
        dir = "up";
    else if ((event.keyCode == 39 || event.keyCode == 68) && dir != "left")
        dir = "right";
    else if ((event.keyCode == 40 || event.keyCode == 83) && dir != "up")
        dir = "down";
}

function eatTail(head, arr) {
    for (let i = 0; i < arr.length; i++) {
        if (head.x == arr[i].x && head.y == arr[i].y) {
            ctx.fillStyle = "red";
            ctx.fillRect(snake[0].x, snake[0].y, step, step);
            return true;

        }
    }
}

function edge(head) {
    if (head.x < step || head.x > step * 38
        || head.y < 2 * step || head.y > step * 20 + step * 0.5) {
        ctx.fillStyle = "red";
        ctx.fillRect(snake[0].x, snake[0].y, step, step);
        /////clearInterval(game);
        return true;

    }
}

let s = Math.floor(Math.random() * 10 + 10)


function draw_elements() {
    ctx.drawImage(background, 0, 0);
    ctx.drawImage(apple_img, food.x, food.y)

    ctx.fillStyle = "#007531";
    ctx.fillRect(snake[0].x, snake[0].y, step, step);

    if (snake[0].x < step || snake[0].x > step * 38
        || snake[0].y < 2 * step || snake[0].y > step * 20 + step * 0.5) {
        ctx.fillStyle = "red";
        ctx.fillRect(snake[0].x, snake[0].y, step, step);
        /////clearInterval(game);
        saveGame(score);
        localStorage.getItem("score")
        //setTimeout(function () {
        //    alert("GAME OVER!");
        //}, 10);
        document.location.reload();
        ////clearInterval(game);

    }


    for (let i = 1; i < snake.length; i++) {
        ctx.fillStyle = "green";
        ctx.fillRect(snake[i].x, snake[i].y, step, step);
    }
    ctx.fillStyle = "white";
    ctx.font = "60px Arial";
    ctx.fillText("Score:", step * 1.25, step * 1.5)
    ctx.fillText(score, step * 5, step * 1.5)

    ctx.fillText("Length:", step * 10, step * 1.5)
    ctx.fillText(snake.length, step * 15, step * 1.5)

    let snakeX = snake[0].x;
    let snakeY = snake[0].y;

    if (snake[0].x < step || snake[0].x > step * 38
        || snake[0].y < 2 * step || snake[0].y > step * 20 + step * 0.5) {
        ctx.fillStyle = "red";
        ctx.fillRect(snake[0].x, snake[0].y, step, step);
        ////clearInterval(game);


        //setTimeout(function () {
        //    alert("GAME OVER!");
        //}, 10);
        saveGame();
        document.location.reload();
        ///clearInterval(game);

    }


    if (snakeX == food.x && snakeY == food.y) {
        score++;
        food = {
            x: Math.floor(Math.random() * 25 + 1) * step, // формирует значение от 1 до 25(кол-во элементов в сторке фон)
            y: Math.floor(Math.random() * 12 + 3) * step,
        };
    }
    else {
        snake.pop();
    }

    if (dir == "left") snakeX -= step;
    if (dir == "right") snakeX += step;
    if (dir == "up") snakeY -= step;
    if (dir == "down") snakeY += step;


    let newHead = {
        x: snakeX,
        y: snakeY
    };



    if (eatTail(newHead, snake) || edge(newHead)) {

        //setTimeout(function () {
        //    alert("GAME OVER!");
        //}, 10);
        saveGame();
        document.location.reload();
        //clearInterval(game);

    }

    snake.unshift(newHead); //поместить в начало

    if (score == 10 || score == 22 || score == 35 || snake.length > 30) {
        ////    if (store == s){
        ////while (snakeX != treat.x && snakeY != treat.y){
        ctx.drawImage(treat_img, treat.x, treat.y)//}
        if (snakeX == treat.x && snakeY == treat.y) {
            score += 2;
            treat = {
                x: Math.floor(Math.random() * 25 + 1) * step, // формирует значение от 1 до 25(кол-во элементов в сторке фон)
                y: Math.floor(Math.random() * 12 + 3) * step,
            };
            for (let j = 0; j < 3; j++) {
                snake.pop();
            }
        }
    }

}

let game = setInterval(draw_elements, 100) //каждые 100 мс будет перерисовавоться фон

function saveGame(score) {
    ////var xhttp = new XMLHttpRequest();
    ////xhttp.onlyreadystatechange = function () {
    ////    if (this.readyState == 4 && this.status == 200) {
    ////        console.log(score)
    ////    }
    ////}
    ////xhttp.open("GET", "https://localhost:44390/Home/Test");
    ////xhttp.send()
    //////post('SaveGame', { score: countScore }, function () {
    //////    location.reload();
    //////});
    localStorage.setItem("score", score)

}


