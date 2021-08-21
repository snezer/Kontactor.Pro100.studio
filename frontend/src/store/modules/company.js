import APICRMServices from "@/services/APICRMServices";

const state = {
    companyId : null,
    companyInfo: null,
	employees: []
}

const getters = {
	companyId : state=>state.companyId,
    employees: state=>state.employees,
    companyInfo: state=>state.companyInfo
}

const actions = {
	async loadCompany({commit}, companyId){
        commit('SET_COMPANY_ID', companyId);
        var companyInfo = await APICRMServices.getCompany(companyId);
        commit('SET_COMPANY_INFO', companyInfo.data);
        var emps = await APICRMServices.getCompanyEmployees(companyId);
        commit('SET_COMPANY_EMPLOYEES', emps.data);
    }
}

const mutations = {
	SET_EMPLOYEES(state, employees){
        state.employees = employees;
    },
    SET_COMPANY_INFO(state, info){
        state.info = info;
    },
    SET_COMPANY_EMPLOYEES(state, data){
        state.employees = data;
    }
}

export default {
    namespaced: true,
    state,
    getters,
    actions,
    mutations,
}