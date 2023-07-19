package com.vxcompany.kennisdag;
import com.fasterxml.jackson.annotation.JsonIgnore;
import com.fasterxml.jackson.annotation.JsonProperty;
import io.quarkus.hibernate.orm.panache.PanacheEntityBase;
import jakarta.persistence.Entity;
import jakarta.persistence.Id;
import jakarta.persistence.Table;

@Entity
@Table(name = "profile")
public class Profile extends PanacheEntityBase {

    @JsonProperty("nickname")
    public String nickName;
    public String bio;

    @Id
    @JsonIgnore
    public String userId;

    public static Profile findByUserId(String userId) {
        return find("userId", userId).firstResult();
    }

    public static Profile findByNickName(String nickName) {
        return find("nickName", nickName).firstResult();
    }

}
