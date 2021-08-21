<template>
  <div>
    <v-virtual-scroll height="650px" :items="items" item-height="280px" class="messages-wrapper">
      <template v-slot:default="{ }">
        <message ></message>
      </template>
    </v-virtual-scroll>
  </div>
</template>

<script>
import Message from "@/components/Dashboard/Message";
export default {
  name: "Messages",
  components: {Message},
  data(){
    return{
      items: [1,2,3,4,5,6,7,8,9,0,10,11,12,13,14,15,16,17,18,19,20,21,22,23]
    }
  }
}
</script>

<style scoped lang="sass">
.messages-wrapper
</style>