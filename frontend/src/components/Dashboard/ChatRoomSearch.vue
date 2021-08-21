<template>
  <div class="search-wrapper">
    <div class="search">
      <v-text-field solo flat full-width label="Поиск" append-icon="mdi-magnify"></v-text-field>
    </div>
  </div>
</template>

<script>
export default {
  name: "ChatRoomSearch"
}
</script>

<style scoped lang="sass">
.search-wrapper
  width: 100%
  height: 100%
  display: flex
  justify-content: center
  align-items: center
.search
  max-height: 64px
  width: 96%
  margin-left: 2%
</style>