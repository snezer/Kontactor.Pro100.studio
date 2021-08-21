import APICRMServices from "@/services/APICRMServices";


const state = {
    rents: [],
    fullRents: []
}

const getters = {
    allRents: state=>state.rents,
    allRentsFullData: state=>state.fullRents
}

const actions = {
    async loadRents({commit}){
        const rents = await APICRMServices.getRents()
        commit('SAVE_RENTS', rents.data)
    },
    async loadRentsFullData({commit, dispatch, state}){
        await dispatch('loadRents')
        let fullRents = await Promise.all( state.rents.map( async (rent) =>{
            let infoRoom = await APICRMServices.getInfoRoom(rent.compartmentId)
            let infoUser = await APICRMServices.getUser(rent.userId)
            // let infoCompany = await APICRMServices.getCompany(rent.companyId)
            return {
                ...rent,
                time: `${rent.contractStart} - ${rent.contractEnd}`,
                place: infoRoom.data.name,
                name: `${infoUser.data.lastName} ${infoUser.data.firstName}`,
            }
        }))
        commit('SAVE_RENTS_FULL_DATA', fullRents)
    }
}
const mutations = {
    SAVE_RENTS(state, payload){
        state.rents = payload
    },
    SAVE_RENTS_FULL_DATA(state, payload){
        state.fullRents = payload
    }
}

export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
}