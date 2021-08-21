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
      items: [1,2,3,4,5,6,7,8,9,0,10,11,12,13,14,15,16,17,18,19,20,21,22,23]
    }
  }
}
</script>

<style scoped>

</style>