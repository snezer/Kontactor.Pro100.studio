import APICRMServices from "@/services/APICRMServices";

const state = {
	userid: null,
	companyid:null,
	tenantid:null,
	lastName : null,
	firstName: null,
	middleName: null,
	authenticated: false,
	isEmployee:false,
	companyname:null,
	roles: [],
	isUK: false

}

const getters = {
	isAuthenticated : state => {
        return state.authenticated
    },

	userId : state=> {
		return state.userid;
	},

	userFIO : state=>{
		return `${state.lastName} ${state.firstName} ${state.middleName!=null ? state.middleName : '' }`;
	},

	isUserEmployee : state => {
		return state.isEmployee;
	},

	companyId : state => {
		return state.companyid;
	},

	companyName : state=> {
		return state.companyname;
	},
	tenantId : state => {
		return state.tenantid;
	},
	isUKPersonnel : state => {
		return state.isUK
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
		state.isEmployee = false;
		state.companyid = null;
		state.tenantid = null;
		state.isUK = false;		
	},
	LOGON(state, user)
	{
		state.firstName = user.firstName;
		state.lastName = user.lastName;
		state.userid = user.id;
		state.middleName = user.middleName;
		state.authenticated = true;
		state.companyid = user.companyId;
		state.isEmployee = user.isEmployee;
		state.tenantid = user.tenantId;
		state.isUK = user.isUK;
	}	
}

export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations,
}