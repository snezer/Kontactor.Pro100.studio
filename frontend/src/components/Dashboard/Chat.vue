<template>
  <div class="chat-wrapper">
      <div class="info-chat">
        <v-list>
          <v-list-item>
            <v-list-item-avatar>
              <v-img src="https://cdn.vuetifyjs.com/images/john.jpg"></v-img>
            </v-list-item-avatar>
            <v-list-item-content>
              <v-list-item-title>Иванов Иван</v-list-item-title>
              <v-list-item-subtitle>Еще какой-то текст</v-list-item-subtitle>
            </v-list-item-content>
            <v-list-item-action>
              <div class="info-chat-action">
                <v-btn icon class="btn-info-chat">
                  <v-icon>
                    mdi-magnify
                  </v-icon>
                </v-btn>
                <v-btn icon class="btn-info-chat">
                  <v-icon>
                    mdi-dots-vertical
                  </v-icon>
                </v-btn>
              </div>
            </v-list-item-action>
          </v-list-item>
        </v-list>
      </div>
      <div class="messages-wrapper">
        <div class="messages">
          <messages></messages>
        </div>
        <div class="input-message">
          <div class="clip">
            <v-btn icon class="btn-info-chat">
              <v-icon>
                mdi-paperclip
              </v-icon>
            </v-btn>
          </div>
          <div class="message-text">
            <div style="max-height: 48px; width: 100%">
              <v-text-field solo flat  label="Введите сообщение..."></v-text-field>
            </div>
          </div>
          <div class="send">
              <v-btn text color="primary">
               Отправить
              </v-btn>
          </div>
        </div>
      </div>
  </div>
</template>

<script>
import Messages from "@/components/Dashboard/Messages";
export default {
  name: "Chat",
  components: {Messages}
}
</script>

<style scoped lang="sass">
.chat-wrapper
  width: 100%
  height: 100%
  display: grid
  grid-template-rows: 75px 1fr
.info-chat
  position: relative
.info-chat::after
  position: absolute
  height: 1px
  width: 99%
  margin-left: .5%
  bottom: 0
  content: ''
  background-color: #eee
.info-chat-action
  display: flex
  gap: 10px
  padding-right: 25px
.btn-info-chat
  background-color: #eee
.messages-wrapper
  display: grid
  grid-template-rows: 1fr 75px
.messages
  border-bottom: 1px #eee solid
  width: 60%
  margin-left: 20%
.input-message
  width: 60%
  margin-left: 20%
  height: 100%
  display: grid
  grid-template-columns: 1fr 10fr 1fr
.input-message .clip
  display: flex
  justify-content: center
  align-items: center
.input-message .message-text
  display: flex
  justify-content: center
  align-items: center
.input-message .send
  display: flex
  justify-content: center
  align-items: center
</style>