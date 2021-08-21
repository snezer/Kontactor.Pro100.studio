import APICRMServices from "@/services/APICRMServices";

const state = {
	userid: null,
	lastName : null,
	firstName: null,
	middleName: null,
	authenticated: false,
	roles: [],

}

const getters = {
	isAuthenticated : state => {
        return state.authenticated
    },

	userId : state=> {
		return state.userid;
	}
}

const actions = {
	logoff({commit}){
		commit('LOGOFF');
	},

	async logon({commit}, {login, password}){
		let userAllowed = await APICRMServices.checkUser(login,password);		
		if (!userAllowed)
		{
			commit('LOGOFF');			
		}		

		let userInfo = await APICRMServices.getUserByLogin(login);
		commit('LOGON', userInfo.data);
	}
}

const mutations = {
	LOGOFF(state){
		state.lastName = null;
		state.firstName = null;
		state.middleName = null;
		state.authenticated = false;
		state.roles = [];
	},
	LOGON(state, user)
	{
		state.firstName = user.firstName;
		state.lastName = user.lastName;
		state.userid = user.id;
		state.middleName = user.middleName;
		state.authenticated = true;
	}	
}

export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations,
}