import axios from "axios";

axios.defaults.baseURL = `https://01fe1d0d97fb.ngrok.io/api/v1/`

async function createUser(user){
    return await axios.post('Users', {...user})
}

async function  createCompany(company){
    return await axios.post('Company', {...company})
}

async function getCompany(id){
    return await axios.get(`Company/${id}`);
}

async function getCompanyEmployees(id){
    return await axios.get(`Company/employees/${id}`);
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
    getUserByLogin,
    getCompany,
    getCompanyEmployees
}