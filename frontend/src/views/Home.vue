<template>
  <div id="home">
    <div class="control-block">
      <router-view name="homeLeftBlock"></router-view>
    </div>
    <div class="main-block">
      <img :src="require('@/assets/homeImage.svg')" alt="">
    </div>
  </div>
</template>

<script>
export default {
  name: 'Home',

}
</script>
<style lang="sass">
@import "src/sass/color"

#home
  height: 100%
  width: 100%
  display: grid
  grid-template-columns: 3fr 8fr

.control-block
  background-color: $blue-dark
  display: flex
  justify-content: center
  align-items: center
.control-block h1
  font-size: 3em
  color: white
.main-block
  background-color: white
  display: flex
  align-items: center
  justify-content: center
</style>
