<template>
  <div class="messenger-wrapper">
    <div class="messenger-chat-room">
      <chat-room></chat-room>
    </div>
    <div class="messenger-chat">
      <chat></chat>
    </div>
  </div>
</template>

<script>
import Chat from "@/components/Dashboard/Chat";
import ChatRoom from "@/components/Dashboard/ChatRoom";
export default {
  name: "Messenger",
  components: {ChatRoom, Chat}
}
</script>

<style lang="sass">
@import "src/sass/color"
.messenger-wrapper
  position: relative
  width: 100%
  height: 100%
  display: grid
  grid-template-columns: 3fr 10fr

.messenger-chat-room
  position: relative
.messenger-chat-room::after
  position: absolute
  top: 5px
  right: 3px
  width: 1px
  height: 100%
  content: ''
  background-color: #eee
  .messenger-chat
    height: 100%
</style>