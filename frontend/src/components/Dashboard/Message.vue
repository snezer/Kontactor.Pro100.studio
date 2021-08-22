<template>
  <div class="message-wrapper">
    <div class="avatar">
        <v-img class="v-avatar" width="50px" src="https://cdn.vuetifyjs.com/images/john.jpg"></v-img>
    </div>
    <div class="messages-from-people">
      <div class="body-message blue-message">
        <div>
<!--          <div class="title">Иванов Иван</div>-->
          <div class="message">Таким образом реализация намеченных плановых заданий позволяет выполнять важные задания по разработке дальнейших направлений развития.</div>
        </div>
        <div class="time-message">
          17:50
        </div>
      </div>
      <div class="body-message default-message">
        <div>
          <div class="title">Иванов Иван</div>
          <div class="message">Товарищи! начало повседневной работы по формированию позиции позволяет выполнять важные задания по разработке позиций, занимаемых участниками в отношении поставленных задач. Задача организации, в особенности же новая модель организационной деятельности влечет за собой процесс внедрения и модернизации соответствующий условий активизации. </div>
        </div>
        <div class="time-message">
          17:54
        </div>
      </div>
    </div>

  </div>
</template>

<script>
export default {
  name: "Message"
}
</script>

<style scoped lang="sass">
@import "src/sass/color"
.message-wrapper
  width: 100%
  display: grid
  grid-template-columns: 50px 1fr
  gap: 5px
  padding-right: 15px
  margin-top: 20px
.avatar
  display: flex
  justify-content: center
  align-items: flex-end
.messages-from-people
  display: flex
  flex-direction: column
  gap: 10px
.body-message
  display: grid
  grid-template-columns: 11fr 1fr
  background-color: #f1f1f175 !important
  padding: 20px 10px
  border-radius: 5px
.title
  font-size: 1em !important
.message
  font-size: .8em
.time-message
  display: flex
  justify-content: flex-end
  align-items: flex-end
  font-size: .8em
.body-message.blue-message
  background-color: $blue-light !important
.body-message.blue-message .title
  color: white !important
.body-message.blue-message .message
  color: white !important
.body-message.blue-message .time-message
  color: white !important
</style>