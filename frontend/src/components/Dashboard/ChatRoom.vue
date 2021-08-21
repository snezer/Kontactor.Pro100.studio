<template>
  <div class="chat-room-main-wrapper">
    <div class="chat-room-search-wrapper">
      <chat-room-search ></chat-room-search>
    </div>
    <div class="chat-room">
      <chat-room-channels></chat-room-channels>
    </div>
  </div>
</template>

<script>
import ChatRoomSearch from "@/components/Dashboard/ChatRoomSearch";
import ChatRoomChannels from "@/components/Dashboard/ChatRoomChannels";
export default {
  name: "ChatRoom",
  components: {ChatRoomChannels, ChatRoomSearch}
}
</script>

<style scoped lang="sass">
  .chat-room-main-wrapper
    width: 100%
    height: 100%
    display: grid
    grid-template-rows: 75px 1fr
  .chat-room-search-wrapper
    position: relative
  .chat-room-search-wrapper::after
    position: absolute
    bottom: 0
    left: 0
    height: 1px
    width: 99%
    background-color: #eee
    content: ''
  .chat-room
    height: 100%
    overflow-y: auto
</style>