<template>
  <div id="login">
    <v-row>
      <v-col cols="12">
        <v-img width="50%" style="margin-left: 25%;" :src="require('@/assets/logo (3).svg')"></v-img>
      </v-col>
      <v-col lg="6" offset-lg="3">
        <v-text-field dark outlined v-model="login" label="Логин" color="white" dense append-icon="mdi-account"></v-text-field>
      </v-col>
      <v-col lg="6" offset-lg="3">
        <v-text-field dark type="password" v-model="password" outlined label="Пароль" color="white" dense append-icon="mdi-key"></v-text-field>
      </v-col>
      <v-col lg="6" offset-lg="3" class="text-lg-center">
        <v-btn :loading="loading" outlined color="white" @click="checkUser">
          Войти
        </v-btn>
      </v-col>
    </v-row>
  </div>
</template>

<script>
import APICRMServices from "@/services/APICRMServices";
import {mapActions, mapGetters} from 'vuex';

export default {
  name: "HomeLogin",
  data(){
    return{
      login: '',
      password: '',
      loading: false,
    }
  },
  computed:{
    ...mapGetters({isAuthenticated:'user/isAuthenticated'})
  },
  methods:
    {
      async checkUser(){
        this.loading = true;
        let self = this;
        const result = await this.logon({login: self.login, password: self.password})
        this.loading = false
        if (this.isAuthenticated){
          this.$router.push({name:'accruals'})
        }
      },
      async logon(credentials){
        await this.$store.dispatch('user/logon', credentials);
      }      
    }
    
  
}
</script>

<style lang="sass">
@import '~vuetify/src/styles/styles.sass'
$text-field-outlined-fieldset-border-width: 2px
</style>