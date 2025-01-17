// Copyright 2016 Proyectos y Sistemas de Mantenimiento SL (eProsima).
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

/*!
 * @file TwistPubSubTypes.cpp
 * This header file contains the implementation of the serialization functions.
 *
 * This file was generated by the tool fastcdrgen.
 */

#include <fastcdr/FastBuffer.h>
#include <fastcdr/Cdr.h>

#include "TwistPubSubTypes.h"

using SerializedPayload_t = eprosima::fastrtps::rtps::SerializedPayload_t;
using InstanceHandle_t = eprosima::fastrtps::rtps::InstanceHandle_t;

namespace geometry_msgs {
    namespace msg {
        // TwistPubSubType类的构造函数
        TwistPubSubType::TwistPubSubType()
        {
            // 设置类型名称
            setName("geometry_msgs::msg::dds_::Twist_");
            // 获取Twist类型的最大CDR序列化大小
            auto type_size = Twist::getMaxCdrSerializedSize();
            // 进行可能的子消息对齐（按照4字节对齐）
            type_size += eprosima::fastcdr::Cdr::alignment(type_size, 4); /* 可能的子消息对齐 */
            // 计算类型大小，加上封装所需的4字节
            m_typeSize = static_cast<uint32_t>(type_size) + 4; /*封装*/
            // 检查Twist类型是否定义了获取键（key）的操作
            m_isGetKeyDefined = Twist::isKeyDefined();
            // 根据Twist类型获取键的最大CDR序列化大小来确定键缓冲区的长度，若大于16则取其自身大小，否则取16
            size_t keyLength = Twist::getKeyMaxCdrSerializedSize() > 16?
                    Twist::getKeyMaxCdrSerializedSize() : 16;
            // 分配键缓冲区内存
            m_keyBuffer = reinterpret_cast<unsigned char*>(malloc(keyLength));
            // 将键缓冲区的内存初始化为0
            memset(m_keyBuffer, 0, keyLength);
        }

        // TwistPubSubType类的析构函数
        TwistPubSubType::~TwistPubSubType()
        {
            // 如果键缓冲区不为空，则释放其内存
            if (m_keyBuffer!= nullptr)
            {
                free(m_keyBuffer);
            }
        }

        // 序列化函数，用于将给定的数据对象序列化为SerializedPayload_t类型
        bool TwistPubSubType::serialize(
                void* data,
                SerializedPayload_t* payload)
        {
            // 将传入的void*类型数据转换为Twist*类型指针，方便后续操作
            Twist* p_type = static_cast<Twist*>(data);

            // 创建一个FastBuffer对象，用于管理原始缓冲区，它包装了SerializedPayload_t中的数据缓冲区
            eprosima::fastcdr::FastBuffer fastbuffer(reinterpret_cast<char*>(payload->data), payload->max_size);
            // 创建一个Cdr对象，用于对数据进行序列化操作，指定了字节序和相关的CDR模式
            eprosima::fastcdr::Cdr ser(fastbuffer, eprosima::fastcdr::Cdr::DEFAULT_ENDIAN, eprosima::fastcdr::Cdr::DDS_CDR);
            // 根据序列化对象的字节序来设置SerializedPayload_t中的封装字节序标识（大端序或小端序）
            payload->encapsulation = ser.endianness() == eprosima::fastcdr::Cdr::BIG_ENDIANNESS? CDR_BE : CDR_LE;
            // 序列化封装信息（可能包含头部等相关封装数据）
            ser.serialize_encapsulation();

            try
            {
                // 调用Twist对象的serialize函数，将Twist对象的实际数据序列化到Cdr对象管理的缓冲区中
                p_type->serialize(ser);
            }
            catch (eprosima::fastcdr::exception::NotEnoughMemoryException& /*exception*/)
            {
                // 如果出现内存不足异常，则返回false表示序列化失败
                return false;
            }

            // 获取序列化后数据的长度，并设置到SerializedPayload_t对象中
            payload->length = static_cast<uint32_t>(ser.getSerializedDataLength());
            return true;
        }

        // 反序列化函数，用于将SerializedPayload_t类型的数据反序列化为对应的数据对象
        bool TwistPubSubType::deserialize(
                SerializedPayload_t* payload,
                void* data)
        {
            try
            {
                // 将传入的void*类型数据转换为Twist*类型指针，用于接收反序列化后的数据
                Twist* p_type = static_cast<Twist*>(data);

                // 创建一个FastBuffer对象，用于管理原始缓冲区，其长度为传入的SerializedPayload_t中包含的实际有效数据长度
                eprosima::fastcdr::FastBuffer fastbuffer(reinterpret_cast<char*>(payload->data), payload->length);

                // 创建一个Cdr对象，用于对数据进行反序列化操作，指定了字节序和相关的CDR模式
                eprosima::fastcdr::Cdr deser(fastbuffer, eprosima::fastcdr::Cdr::DEFAULT_ENDIAN, eprosima::fastcdr::Cdr::DDS_CDR);

                // 先反序列化封装信息（可能包含头部等相关封装数据）
                deser.read_encapsulation();
                // 根据反序列化对象的字节序来设置SerializedPayload_t中的封装字节序标识（大端序或小端序）
                payload->encapsulation = deser.endianness() == eprosima::fastcdr::Cdr::BIG_ENDIANNESS? CDR_BE : CDR_LE;

                // 调用Twist对象的deserialize函数，将Cdr对象管理的缓冲区中的数据反序列化为Twist对象
                p_type->deserialize(deser);
            }
            catch (eprosima::fastcdr::exception::NotEnoughMemoryException& /*exception*/)
            {
                // 如果出现内存不足异常，则返回false表示反序列化失败
                return false;
            }

            return true;
        }

        // 返回一个函数对象，该函数对象用于获取给定数据对象序列化后的大小（包含封装部分）
        std::function<uint32_t()> TwistPubSubType::getSerializedSizeProvider(
                void* data)
        {
            return [data]() -> uint32_t
                   {
                       // 返回Twist对象序列化后的大小（通过调用其相关函数获取）再加上封装所需的4字节
                       return static_cast<uint32_t>(type::getCdrSerializedSize(*static_cast<Twist*>(data))) +
                              4u /*封装*/;
                   };
        }

        // 创建一个Twist类型的数据对象，并返回其void*指针形式（用于后续操作中传递该对象指针）
        void* TwistPubSubType::createData()
        {
            return reinterpret_cast<void*>(new Twist());
        }

        // 删除给定的Twist类型的数据对象（通过先将void*指针转换为Twist*指针，再调用delete进行内存释放）
        void TwistPubSubType::deleteData(
                void* data)
        {
            delete(reinterpret_cast<Twist*>(data));
        }

        // 获取给定数据对象（Twist类型）的键（key）信息，并设置到InstanceHandle_t对象中
        bool TwistPubSubType::getKey(
                void* data,
                InstanceHandle_t* handle,
                bool force_md5)
        {
            // 如果没有定义获取键的操作，则直接返回false
            if (!m_isGetKeyDefined)
            {
                return false;
            }

            Twist* p_type = static_cast<Twist*>(data);

            // 创建一个FastBuffer对象，用于管理键缓冲区，其长度为Twist类型获取键的最大CDR序列化大小
            eprosima::fastcdr::FastBuffer fastbuffer(reinterpret_cast<char*>(m_keyBuffer),
                    Twist::getKeyMaxCdrSerializedSize());

            // 创建一个Cdr对象，用于对键数据进行序列化操作，这里指定为大端序
            eprosima::fastcdr::Cdr ser(fastbuffer, eprosima::fastcdr::Cdr::BIG_ENDIANNESS);
            // 调用Twist对象的serializeKey函数，将键相关数据序列化到Cdr对象管理的缓冲区中
            p_type->serializeKey(ser);
            if (force_md5 || Twist::getKeyMaxCdrSerializedSize() > 16)
            {
                // 如果强制使用MD5或者键的最大CDR序列化大小大于16字节，则进行MD5相关操作
                m_md5.init();
                m_md5.update(m_keyBuffer, static_cast<unsigned int>(ser.getSerializedDataLength()));
                m_md5.finalize();
                for (uint8_t i = 0; i < 16; ++i)
                {
                    handle->value[i] = m_md5.digest[i];
                }
            }
            else
            {
                // 否则直接将键缓冲区中的前16字节数据复制到InstanceHandle_t对象中
                for (uint8_t i = 0; i < 16; ++i)
                {
                    handle->value[i] = m_keyBuffer[i];
                }
            }
            return true;
        }
    } //End of namespace msg
} //End of namespace geometry_msgs
