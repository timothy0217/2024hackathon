// Import the functions you need from the SDKs you need
import { initializeApp } from "firebase/app";
import { getDatabase, ref } from 'firebase/database';
import { getFirestore } from "firebase/firestore";

// TODO: Add SDKs for Firebase products that you want to use
// https://firebase.google.com/docs/web/setup#available-libraries

// Your web app's Firebase configuration
const firebaseConfig = {
  apiKey: "AIzaSyD6ULCsO-cG1W_TuQtBIpQ0KEeyW_Cu9LE",
  authDomain: "hackathon2024-1b87f.firebaseapp.com",
  databaseURL: "https://hackathon2024-1b87f-default-rtdb.asia-southeast1.firebasedatabase.app",
  projectId: "hackathon2024-1b87f",
  storageBucket: "hackathon2024-1b87f.appspot.com",
  messagingSenderId: "87705706406",
  appId: "1:87705706406:web:8e227985bb1196c2b63967"
};

// Initialize Firebase
const app = initializeApp(firebaseConfig)
// Initialize Realtime Database and get a reference to the service
//使用Realtime Database
const database = getDatabase(app)// 建立資料庫ref
//使用FireStore
export const db=getFirestore(app)
export const realtimeRef = ref(database, 'chatroom')
export const writedb=getFirestore(app)
// 使用不同路徑(table)
// export const usersRef = ref(database, 'users')
// 使用不同路徑(table)
// export const messagesRef = ref(database, 'messages')

// 可用function https://firebase.google.com/docs/database/web/lists-of-data?hl=zh-tw
// 假設你想篩選出 age 等於 25 的所有記錄
// const filteredQuery = query(realtimeRef, orderByChild('age'), equalTo(25));

// get(filteredQuery).then((snapshot) => {
//     if (snapshot.exists()) {
//         console.log(snapshot.val());
//     } else {
//         console.log('No data available');
//     }
// }).catch((error) => {
//     console.error(error);
// });
