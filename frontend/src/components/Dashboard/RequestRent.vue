<template>
  <div class="request-wrapper">
    <div class="menu">

    </div>
    <div class="list-request-rent">
      <h2>Список заявок на аренду</h2>
    </div>
  </div>
</template>

<script>
export default {
  name: "RequestRent"
}
</script>

<style scoped lang="sass">
.request-wrapper
  width: 100%
  height: 100%
  display: grid
  grid-template-columns: 2fr 11fr
.menu
  position: relative
.menu::after
  position: absolute
  top: 5px
  right: 3px
  width: 1px
  height: 100%
  content: ''
  background-color: #eee
.list-request-rent
  padding: 50px
</style>