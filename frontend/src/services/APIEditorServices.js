import axios from '@/plugins/axios'
axios.defaults.baseURL = `https://babelapi.rb.asu.ru/`

async function getAllHomeElements() {
    return await axios.get('/room')
}

async function setHomeElements(homeElement) {
    if (homeElement.id){
        return await  axios.put('/room/'+homeElement.id, {
            level: homeElement.floor,
            type: homeElement.type,
            positionStart: {
                x: homeElement.positionStart.x,
                y: homeElement.positionStart.y
            },
            size: {
                width: homeElement.size.width,
                height: homeElement.size.height
            },
            description: homeElement.description,
            name: homeElement.name
        })
    } else {
        return await  axios.post('/room', {
            level: homeElement.floor,
            type: homeElement.type,
            positionStart: {
                x: homeElement.positionStart.x,
                y: homeElement.positionStart.y
            },
            size: {
                width: homeElement.size.width,
                height: homeElement.size.height
            },
            description: homeElement.description,
            name: homeElement.name
        })
    }

}

function deleteHomeElement(idHomeElement) {
    return axios.delete('/room/'+idHomeElement)
}

async function getFloors() {
    return await axios.get('/level')
}

function addFloors(floors) {
    return axios.post('/level', {
        title: floors.title,
        value: floors.value,
    })
}
async function addImageFloor(level,data) {
    return await axios.post('/level/background/'+level,
        data,
        {
            headers: {
                'Content-Type': 'multipart/form-data'
            }
        })
}

async function deleteFloor(idLevel) {
    return await axios.delete('/level/'+idLevel)
}

async function searchPath(source, target) {
    console.log(source, target)
    return await axios.get('/path',{
        params:{
            sourceRoomName: source,
            targetRoomName: target
        }

    })
}
async function getAllNode(level){
    return await axios.get('/entity/'+level)
}
async function addNode(data) {
    return await axios.post('/entity/'+data.floor,{
        position: {
            x: data.positionStart.x,
            y: data.positionStart.y
        },
        type: data.type,
    })
}
 async function deleteNode(id){
    return await axios.delete('/entity/'+id)
 }
async function getBooks(id, au) {
    return await axios.post('/book/find', {
        iddb: '4',
        ID: id,
        AU: au,
        TI: '',
        PY: '',
        PU: '',
        PP: '',
        RUSMARC: ''
    })
}
export default {
    getAllHomeElements,
    setHomeElements,
    deleteHomeElement,
    addFloors,
    getFloors,
    addImageFloor,
    deleteFloor,
    searchPath,
    addNode,
    getAllNode,
    getBooks,
    deleteNode
}
