<template>
  <div class="chat-room-item">
    <v-list-item>
      <v-list-item-avatar>
        <v-img src="https://cdn.vuetifyjs.com/images/john.png"></v-img>
      </v-list-item-avatar>
      <v-list-item-content>
        <v-list-item-title>Иванов Иван</v-list-item-title>
        <v-list-item-subtitle>Lorem ipsum dolor sit amet...</v-list-item-subtitle>
      </v-list-item-content>
      <v-list-item-action >
        <div class="info-channel-messages">
            <div>17:54</div>
            <div>
              <v-badge content="13" color="red" class="color-badge-count-messages"></v-badge>
            </div>
        </div>
      </v-list-item-action>
    </v-list-item>
  </div>
</template>

<script>
export default {
  name: "ChatRoomChannel"
}
</script>

<style scoped lang="sass">
@import "src/sass/color.sass"

.color-badge-count-messages
  background-color: $red-dark
.info-channel-messages
  height: 48px
  display: flex
  flex-direction: column
  justify-content: center
  align-items: center
.chat-room-item
  position: relative

.chat-room-item::after
  position: absolute
  bottom: 0
  width: 96%
  height: 1px
  margin-left: 2px
  background-color: #eee
  content: ''
</style>