// alphabet.mjs

import fetch from "node-fetch";
import fs from "fs";

const API_KEY = "ruqiuJKIpiJhvWgk8kwYfnUCnhrBYeCFltPBFxp3VqXcZnL7Sd4IKRsc";

const alphabetData = [
  { letter: "A", word: "Apple" },
  { letter: "B", word: "Ball" },
  { letter: "C", word: "Cat" },
  { letter: "D", word: "Dog" },
  { letter: "E", word: "Elephant" },
  { letter: "F", word: "Fish" },
  { letter: "G", word: "Goat" },
  { letter: "H", word: "Hat" },
  { letter: "I", word: "Ice cream" },
  { letter: "J", word: "Jug" },
  { letter: "K", word: "Kite" },
  { letter: "L", word: "Lion" },
  { letter: "M", word: "Monkey" },
  { letter: "N", word: "Nest" },
  { letter: "O", word: "Orange" },
  { letter: "P", word: "Parrot" },
  { letter: "Q", word: "Queen" },
  { letter: "R", word: "Rabbit" },
  { letter: "S", word: "Sun" },
  { letter: "T", word: "Tiger" },
  { letter: "U", word: "Umbrella" },
  { letter: "V", word: "Van" },
  { letter: "W", word: "Watch" },
  { letter: "X", word: "Xylophone" },
  { letter: "Y", word: "Yak" },
  { letter: "Z", word: "Zebra" }
];

async function fetchAndSaveData() {
  const resultArray = [];

  for (const item of alphabetData) {
    let imgUrl = "images/default.jpg";

    try {
      const response = await fetch(`https://api.pexels.com/v1/search?query=${encodeURIComponent(item.word)}&per_page=1`, {
        headers: { Authorization: API_KEY }
      });

      if (!response.ok) {
        throw new Error(`HTTP ${response.status} - ${response.statusText}`);
      }

      const data = await response.json();
      if (data.photos.length > 0) {
        imgUrl = data.photos[0].src.medium;
      } else {
        console.warn(`⚠️ No image found for '${item.word}'`);
      }
    } catch (err) {
      console.error(`Error for ${item.word}:`, err.message);
    }

    resultArray.push({
      letter: item.letter,
      word: item.word,
      imageUrl: imgUrl
    });
  }

  const csvContent = "letter,word,imageUrl\n" + resultArray.map(e => `${e.letter},${e.word},${e.imageUrl}`).join("\n");

  fs.writeFileSync("alphabets.csv", csvContent, "utf8");
  console.log("✅ File saved as alphabets.csv");
}

fetchAndSaveData();
