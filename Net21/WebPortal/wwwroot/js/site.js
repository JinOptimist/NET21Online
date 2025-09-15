var age1 = 18; // BAD Outdate
let age2 = 20; // Good
const age3 = 30; // Tip do not change variable

const name1 = "Ivan";
const name2 = 'Lera';
const name3 = `Lera ${age1}`;

const isAdult = true;

const money = 1.1; // number
const age = 30; // number
const numberButNot = NaN; // number

const emptyUser = null;
const alsoEmptyUser = undefined;

let magic = 15; // number
magic = true; // boolean
magic = "Ivan"; // string

let user = {
    name: "User1",
    age: 99,
    friednIds: [1, 23, 6],
    adress: {
        street: "Lenina",
        house: 12
    },
    cards: [{
        isVise: true,
        amount: 50
    },
    {
        isVise: false,
        amount: 10
    },
    ]
};

user.age = 50;
user.adress = "Test";

magic = 12;
magic.smile = "qwe";

if (isAdult) {
    console.log("18+")
} else {
    console.log("18-");
}

if (user) {
    console.log(user.name);
} else {
    console.log("user is not created");
}

let text = '50';
console.log(text + 'smile') // '50smile'
console.log(text + 10) // '5010'
console.log(text - 10) // 40
let num = text - 0; // 50


function MulTo2(num1) {
    const answer = num1 * 2;
    console.log(num1 * 2);
    return answer;
}
let number3 = MulTo2(60);

let MulTo2V2 = function (num1) {
    const answer = num1 * 2;
    console.log(num1 * 2);
    return answer;
}
let number4 = MulTo2V2(40);

let MulTo2V3 = (num1) => {
    const answer = num1 * 2;
    console.log(num1 * 2);
    return answer;
}
let number5 = MulTo2V3(50);

function SayHi() {
    console.log("Hi " + this.name);
}
user.name = "cool";
user.Hi = SayHi;
user.Hi();

const array = [12, 'qwe', 2];
for (let i = 0; i < array.length; i++) {
    const element = array[i];
    console.log(element);
}
