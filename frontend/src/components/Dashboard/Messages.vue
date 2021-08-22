<template>
  <div style="display: flex; justify-content: center; align-items: flex-end; height: 100%; padding-bottom: 25px">
        <message ></message>
  </div>
</template>

<script>
import Message from "@/components/Dashboard/Message";
export default {
  name: "Messages",
  components: {Message},
  data(){
    return{
      items: [1]
    }
  }
}
</script>

<style scoped lang="sass">
.messages-wrapper
</style>