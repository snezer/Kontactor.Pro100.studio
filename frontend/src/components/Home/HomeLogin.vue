<template>
  <div id="login">
    <v-row>
      <v-col cols="12">
        <v-img width="50%" style="margin-left: 25%;" :src="require('@/assets/logo (3).svg')"></v-img>
      </v-col>
<!--      <v-col cols="12">-->
<!--        <h1 class="font-weight-black text-lg-center">Контактор</h1>-->
<!--      </v-col>-->
      <v-col lg="6" offset-lg="3">
        <v-text-field dark outlined label="Логин" color="white" dense append-icon="mdi-account"></v-text-field>
      </v-col>
      <v-col lg="6" offset-lg="3">
        <v-text-field dark type="password" outlined label="Пароль" color="white" dense append-icon="mdi-key"></v-text-field>
      </v-col>
      <v-col lg="6" offset-lg="3" class="text-lg-center">
        <v-btn outlined color="white">
          Войти
        </v-btn>
      </v-col>
    </v-row>
  </div>
</template>

<script>
export default {
  name: "HomeLogin"
}
</script>

<style lang="sass">
@import '~vuetify/src/styles/styles.sass'
$text-field-outlined-fieldset-border-width: 2px
</style>