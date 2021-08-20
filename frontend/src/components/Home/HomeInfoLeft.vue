<template>
  <div class="info-left-block">
    <v-icon color="white" style="font-size: 7em">
      mdi-penguin
    </v-icon>
    <h1 class="font-weight-bold">Контактор</h1>
    <div class="description">
      Lorem ipsum dolor sit amet, consectetur adipisicing elit. Consectetur eius placeat quam rem tenetur. Asperiores autem cum deleniti dicta facere magni molestias nisi nobis provident, quis temporibus unde.
    </div>
    <v-btn elevation="0" width="200" outlined color="white" rounded link to="/login">
        Войти
    </v-btn>
    <div class="registration-block">
      <router-link to="/registration" class="link">
        Присоединиться
      </router-link>
    </div>
  </div>
</template>

<script>
export default {
  name: "HomeInfoLeft"
}
</script>

<style lang="sass">
.info-left-block
  text-align: center
  color: white
  padding: 0 50px
.description
  padding: 25px
.registration-block
  padding: 10px
.link
  color: rgba(255,255,255,.7) !important
  text-decoration: none
</style>