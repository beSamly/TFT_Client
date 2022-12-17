using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Network
{
    namespace PacketId
    {
        enum Prefix
        {
            AUTH = 1,
            MATCH
        }

        enum Auth
        {
            LOGIN_REQ = 1,
            LOGIN_RES
        }

        enum Match
        {
            MATCH_REQ = 1,
            MATCH_REQ_RES,
            PENDING_MATCH_CREATED_SEND,
            PENDING_MATCH_CANCELED_SEND,
            MATCH_ACCEPT_REQ,
            MATCH_ACCEPT_RES,
            MATCH_CANCEL_REQ,
            MATCH_CANCEL_RES,
            MATCH_CREATED_SEND
        }
    }
}
