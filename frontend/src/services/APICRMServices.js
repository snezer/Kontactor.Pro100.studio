import axios from "axios";

axios.defaults.baseURL = `https://localhost:50000/api/v1/`

async function createUser(user){
    return await axios.post('Users', {...user})
}

async function  createCompany(company){
    return await axios.post('Company', {...company})
}

async function getUserByLogin(login){
    return await axios.get(`Users/${login}`);
}

async function checkUser(login, password){
    return await axios.post('Users/check',{login: login, password: password})
}



async function saveInfoRoom(infoRoom){

    return await axios.post('Compartment', {...infoRoom})
}
async function getInfoRoom(idRoom){
    return await axios.get(`Compartment/${idRoom}`)
}
async function getInfoRoomByMapId(mapRoomId){
    return await axios.get(`Compartment/mapid/${mapRoomId}`)
}
export default {
    createCompany,
    checkUser,
    saveInfoRoom,
    getInfoRoom,
    getInfoRoomByMapId,
    getUserByLogin
}