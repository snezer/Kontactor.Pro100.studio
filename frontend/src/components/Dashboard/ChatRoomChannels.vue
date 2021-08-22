<template>
  <div>
    <v-virtual-scroll height="800px" :items="items" item-height="75px">
      <template v-slot:default="{ item }">
        <chat-room-channel :key="item"></chat-room-channel>
      </template>
    </v-virtual-scroll>
  </div>
</template>

<script>
import ChatRoomChannel from "@/components/Dashboard/ChatRoomChannel";
export default {
  name: "ChatRoomChannels",
  components: {ChatRoomChannel},
  data(){
    return{
      items: [1]
    }
  }
}
</script>

<style scoped>

</style>